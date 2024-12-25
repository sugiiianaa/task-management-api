using TaskManagement.Application.Interfaces;
using TaskManagement.Application.Models.LoginIO;
using TaskManagement.Application.Models.RegisterIO;
using TaskManagement.Domain.Dtos;
using TaskManagement.Domain.Enums;
using TaskManagementAPI.Constants;
using TaskManagementAPI.Models;
using TaskManagementAPI.Models.Login;
using TaskManagementAPI.Models.Register;

namespace TaskManagementAPI.Endpoints
{
    public static class AuthEndpoints
    {
        public static void MapAuthEndpoints(this WebApplication app)
        {
            app.MapPost("/api/v1/auth/register", async (IUserService userService, RegisterRequest request) =>
            {
                var requestDto = new RegisterInput
                {
                    User = new UserDto
                    {
                        Name = request.Name,
                        Email = request.Email,
                        Password = request.Password,
                        Role = UserRoles.User
                    }
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

                return Results.Created(
                    "/api/v1/resource",
                    new ApiResponse<RegisterResponse>
                    {
                        IsSuccess = true,
                        Message = ResponseMessage.Success,
                        Data = new RegisterResponse
                        {
                            Email = request.Email,
                            Id = response.Id
                        }
                    }
                );
            });

            app.MapPost("/api/v1/auth/login", async (IUserService userService, LoginRequest request) =>
            {
                var requestDto = new LoginInput
                {
                    Email = request.Email,
                    Password = request.Password,
                };

                var response = await userService.LoginUserAsync(requestDto);

                if (!response.IsSuccess || response.Token == null)
                {
                    return Results.BadRequest(new ApiResponse<string>
                    {
                        IsSuccess = false,
                        Message = ResponseMessage.BadRequest
                    });
                }

                return Results.Ok(new ApiResponse<LoginResponse>
                {
                    IsSuccess = true,
                    Message = ResponseMessage.Success,
                    Data = new LoginResponse
                    {
                        Token = response.Token
                    }
                });
            });
        }
    }
}
