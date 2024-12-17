﻿using System.ComponentModel.DataAnnotations;
using TaskManagement.Application.DTOs;
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
            app.MapPost("/api/auth/register", async (IUserService userService, RegisterRequest request) =>
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
                        Message = ResponseMessage.BadRequest
                    });
                }

                return Results.Ok(new ApiResponse<string>
                {
                    IsSuccess = true,
                    Message = ResponseMessage.Success
                });
            });

            app.MapPost("/api/auth/login", async (IUserService userService, LoginRequest request) =>
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
                    Message = ResponseMessage.Success
                });
            });
        }
    }
}
