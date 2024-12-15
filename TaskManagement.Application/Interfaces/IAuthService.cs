﻿using TaskManagement.Application.DTOs;

namespace TaskManagement.Application.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponse> RegisterAsync(RegisterRequest request);
        Task<AuthResponse> LoginAsync(LoginRequest request);

    }
}
