﻿namespace TaskManagement.Application.Interfaces
{
    public interface ITokenService
    {
        string GenerateJwtToken(string email, string role);
    }
}
