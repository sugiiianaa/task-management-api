using TaskManagement.Application.Interfaces;
using TaskManagement.Application.Services;

namespace TaskManagementAPI.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
        }

        public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
        }

    }
}
