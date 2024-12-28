using TaskManagement.Application.Interfaces;
using TaskManagement.Application.Models.InputModel.Auth;
using TaskManagementAPI.Helper;
using TaskManagementAPI.Models.ApiResponseModel;
using TaskManagementAPI.Models.RequestModel.Auth;

namespace TaskManagementAPI.Endpoints
{
    public static class AuthEndpoints
    {
        public static void MapAuthEndpoints(this WebApplication app)
        {
            app.MapPost("/api/v1/auth/register", async (IAuthService authService, RegisterRequest request) =>
            {
                var input = new RegisterUserInput
                {
                    Email = request.Email,
                    Password = request.Password,
                    ReTypePassword = request.ReTypePassword,
                    Name = request.Name
                };

                var output = await authService.RegisterUserAsync(input);

                if (output.ErrorMessage.HasValue)
                {
                    var apiErrorResponseHelper = new ApiErrorResponseHelper();
                    return apiErrorResponseHelper.SendMessage(output.ErrorMessage.Value);
                }

                return Results.Created(
                    "/api/v1/resource",
                    new ApiSuccessResponse<Guid>
                    {
                        Data = output.Data.Value
                    }
                );
            });

            app.MapPost("/api/v1/auth/login", async (IAuthService authService, LoginRequest request) =>
            {
                var input = new LoginUserInput
                {
                    Email = request.Email,
                    Password = request.Password,
                };

                var output = await authService.LoginUserAsync(input);

                if (output.ErrorMessage.HasValue)
                {
                    var apiErrorResponseHelper = new ApiErrorResponseHelper();
                    return apiErrorResponseHelper.SendMessage(output.ErrorMessage.Value);
                }

                return Results.Ok(new ApiSuccessResponse<string>
                {
                    Data = output.Data
                });
            });
        }
    }
}
