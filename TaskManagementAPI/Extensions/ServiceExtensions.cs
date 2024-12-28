using System.Text;
using AspNetCoreRateLimit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using TaskManagement.Application.Interfaces;
using TaskManagement.Application.Models.AppSettings;
using TaskManagement.Application.Services;
using TaskManagement.Infrastructure.Interfaces;
using TaskManagement.Infrastructure.Persistence;
using TaskManagement.Infrastructure.Repositories;

namespace TaskManagementAPI.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureAppServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Register DbContext
            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

            // Register application and infrastructure services
            services.AddApplicationServices();
            services.AddInfrastructureServices(configuration);

            // Register JwtSettingsDto with validation
            services.Configure<JwtSettings>(configuration.GetSection("Jwt"));
            services.AddSingleton(provider =>
            {
                var options = provider.GetRequiredService<IOptions<JwtSettings>>().Value;

                // Validation logic
                if (string.IsNullOrEmpty(options.Secret) ||
                    string.IsNullOrEmpty(options.ValidIssuer) ||
                    string.IsNullOrEmpty(options.ValidAudience))
                {
                    throw new ArgumentException("Invalid JwtSettings: Ensure 'Secret', 'ValidIssuer', and 'ValidAudience' are configured.");
                }

                return options;
            });

            // Configure Authentication and Authorization
            services.AddJwtAuthentication(configuration);
            services.AddAuthorization();

            // Configure Swagger
            services.AddSwaggerGen();

            // Configure Response Compression
            services.AddResponseCompression(options =>
            {
                options.EnableForHttps = true;
                options.MimeTypes = new[] { "text/plain", "application/json", "text/css", "application/javascript" };
            });

            // Configure Rate Limiting
            services.AddRateLimiting(configuration);
        }

        public static void AddApplicationServices(this IServiceCollection services)
        {
            // Register application services
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ITaskService, TaskService>();
        }

        public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Register infrastructure services
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ITaskRepository, TaskRepository>();
        }

        private static void AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettings = configuration.GetSection("Jwt").Get<JwtSettings>();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtSettings.ValidIssuer,
                        ValidAudience = jwtSettings.ValidAudience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret))
                    };
                });
        }

        private static void AddRateLimiting(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMemoryCache();
            services.AddOptions();
            services.Configure<IpRateLimitOptions>(configuration.GetSection("IpRateLimiting"));
            services.AddInMemoryRateLimiting();
            services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
        }
    }
}
