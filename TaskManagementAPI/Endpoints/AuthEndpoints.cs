using TaskManagement.Application.DTOs.LoginDtos;
using TaskManagement.Application.DTOs.RegisterDtos;
using TaskManagement.Application.Interfaces;
using TaskManagementAPI.Constants;
using TaskManagementAPI.Models;

namespace TaskManagementAPI.Endpoints
{
    public static class AuthEndpoints
    {
        public static void MapAuthEndpoints(this WebApplication app)
        {
            app.MapPost("/api/v1/auth/register", async (IUserService userService, RegisterRequest request) =>
            {
                var requestDto = new RegisterRequestDto
                {
                    Email = request.Email,
                    Name = request.Name,
                    Password = request.Password,
                };

                var response = await userService.RegisterUserAsync(requestDto);

                if (!response.IsSuccess)
                {
                    return Results.BadRequest(new ApiResponse<string>
                    {
                        IsSuccess = false,
                        Message = response.Message ?? "An error occured while process the request"
                    });
                }

                return Results.Ok(new ApiResponse<string>
                {
                    IsSuccess = true,
                    Message = ResponseMessage.Success
                });
            });

            app.MapPost("/api/v1/auth/login", async (IUserService userService, LoginRequest request) =>
            {
                var requestDto = new LoginRequestDto
                {
                    Email = request.Email,
                    Password = request.Password,
                };

                var response = await userService.LoginUserAsync(requestDto);

                if (!response.IsSuccess)
                {
                    return Results.BadRequest(new ApiResponse<string>
                    {
                        IsSuccess = false,
                        Message = ResponseMessage.BadRequest
                    });
                }

                return Results.Ok(new ApiResponse<string>
                {
                    IsSuccess = true,
                    Message = ResponseMessage.Success,
                    Data = response.Token
                });
            });
        }
    }
}
