using System.Text;
using AspNetCoreRateLimit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using TaskManagement.Infrastructure.Persistence;
using TaskManagementAPI.Endpoints;
using TaskManagementAPI.Extensions;
using TaskManagementAPI.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Service Configuration
ConfigureServices(builder);

var app = builder.Build();

// Middleware Configuration
ConfigureMiddleware(app);

app.Run();

void ConfigureServices(WebApplicationBuilder builder)
{
    // Register DbContext
    builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

    // Register services
    builder.Services.AddApplicationServices();
    builder.Services.AddInfrastructureServices(builder.Configuration);

    // Register jwt service
    var jwtSettings = builder.Configuration.GetSection("Jwt");
    
    builder.Services.AddAuthentication(
        options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings["ValidIssuer"],
                ValidAudience = jwtSettings["ValidAudience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Secret"] ?? "Default"))
            };
        });

    builder.Services.AddAuthorization();

    // Swagger configuration
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(options =>
    {
        options.SwaggerDoc("v1", new OpenApiInfo
        {
            Title = "TaskManagement API",
            Version = "v1",
            Description = "A simple API for Task Management"
        });
    });

    // Logging configuration
    builder.Logging.ClearProviders();
    builder.Logging.AddConsole();
    builder.Logging.SetMinimumLevel(LogLevel.Debug);

    // Response Compression
    builder.Services.AddResponseCompression(options =>
    {
        options.EnableForHttps = true;
        options.MimeTypes = new[] { "text/plain", "application/json", "text/css", "application/javascript" };
    });

    // Rate Limiting
    builder.Services.AddMemoryCache();
    builder.Services.AddOptions();
    builder.Services.Configure<IpRateLimitOptions>(builder.Configuration.GetSection("IpRateLimiting"));
    builder.Services.AddInMemoryRateLimiting();
    builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
}

void ConfigureMiddleware(WebApplication app)
{
    // Swagger setup
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "TaskManagement API v1");
            c.RoutePrefix = string.Empty;
        });
    }

    // Apply database migration automatically in development
    if (app.Environment.IsDevelopment())
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        dbContext.Database.Migrate();
    }

    // Error handling
    app.UseExceptionHandler(errorApp =>
    {
        errorApp.Run(async context =>
        {
            context.Response.ContentType = "application/json";
            var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;

            if (exception is BadHttpRequestException badRequestException)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                var errorResponse = new
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = "Invalid request format.",
                    Details = app.Environment.IsDevelopment() ? badRequestException.Message : null // Show details in Development only
                };

                await context.Response.WriteAsJsonAsync(errorResponse);
            }
            else
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                var errorResponse = new
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = exception?.Message ?? "An unexpected error occurred.",
                    Details = app.Environment.IsDevelopment() ? exception?.StackTrace : null // Show details in Development only
                };

                await context.Response.WriteAsJsonAsync(errorResponse);
            }
        });
    });

    //Middlewares
    app.UseHttpsRedirection();
    app.UseResponseCompression();
    app.UseIpRateLimiting();
    app.UseAuthentication();
    app.UseAuthorization();
    app.UseMiddleware<LoggingMiddleware>();

    // Map Endpoints
    app.MapAuthEndpoints();
    app.MapTaskEndpoint();
};