using TaskManagement.Application.DTOs;
using TaskManagement.Application.Interfaces;

namespace TaskManagementAPI.Endpoints
{
    public static class AuthEndpoints
    {
        public static void MapAuthEndpoints(this WebApplication app)
        {
            app.MapPost("/api/auth/register", async (IAuthService authService, RegisterRequest request) =>
            {
                var response = await authService.RegisterAsync(request);

                if (response.Message == "User registered successfully")
                {
                    return Results.Ok(response);
                }

                return Results.BadRequest(response);
            });

            app.MapPost("/api/auth/login", async (IAuthService authService, LoginRequest request) =>
            {
                var response = await authService.LoginAsync(request);

                if (response.Message == "Login successful")
                {
                    return Results.Ok(response);
                }

                return Results.Unauthorized();
            });
        }
    }
}
