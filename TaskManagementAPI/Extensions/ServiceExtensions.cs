using TaskManagement.Application.Interfaces;
using TaskManagement.Application.Services;
using TaskManagement.Infrastructure.Interfaces;
using TaskManagement.Infrastructure.Repositories;

namespace TaskManagementAPI.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            // Register application services, e.g., use cases, services
            services.AddScoped<IUserService, UserService>();
        }

        public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Register infrastructure services like repositories and DbContext
            services.AddScoped<IUserRepository, UserRepository>();
        }

    }
}
