using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using TaskManagement.Infrastructure.Persistence;
using TaskManagementAPI.Endpoints;
using TaskManagementAPI.Extensions;

var builder = WebApplication.CreateBuilder(args);

/// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddApplicationServices(); // Register application services (e.g., use cases)
builder.Services.AddInfrastructureServices(builder.Configuration); // Register infrastructure services

// Swagger config
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    // Configure Swagger generation settings here (optional)
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "TaskManagement API",
        Version = "v1",
        Description = "A simple API for Task Management"
    });
});

// Logging config
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.SetMinimumLevel(LogLevel.Debug);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();  // Enable Swagger middleware to generate the Swagger JSON
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "TaskManagement API v1");  // Point to Swagger JSON
        c.RoutePrefix = string.Empty;  // Set Swagger UI to be at the root (http://localhost:5000 or https://localhost:5001)
    });
}

// Apply any pending migrations automatically
using(var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.Migrate();
}

// Endpoints
app.MapAuthEndpoints();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();