using System.ComponentModel.DataAnnotations;
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
                // TODO : fix issue with api return 400 even the process is success
                // also handle already registered user error 
                var response = await authService.RegisterAsync(request);

                if (response.Message == "User registered successfully")
                {
                    return Results.Ok(response);
                }

                return Results.BadRequest(response);
            });

            app.MapPost("/api/auth/login", async (IAuthService authService, LoginRequest request) =>
            {
                // Validate model manually
                // TODO : create global helper later
                var validationResults = new List<ValidationResult>();
                var validationContext = new ValidationContext(request);
                bool isValid = Validator.TryValidateObject(request, validationContext, validationResults, true);

                if (!isValid)
                {
                    var errors = validationResults.ToDictionary(vr => vr.MemberNames.First(), vr => vr.ErrorMessage);
                    return Results.BadRequest(new { errors });
                }

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
