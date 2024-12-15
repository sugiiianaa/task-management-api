using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Application.DTOs;
using TaskManagement.Application.Interfaces;
using TaskManagement.Domain.Entities;
using TaskManagement.Infrastructure.Interfaces;

namespace TaskManagement.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;

        public AuthService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<AuthResponse> LoginAsync(LoginRequest request)
        {
            var user = await _userRepository.GetUserByUsernameAsync(request.Username);

            // TODO: implement password encryp & decrypt later
            if (user == null || user.PasswordHash != request.Password)
            {
                return new AuthResponse { Message = "invalid credentials" }; 
            }

            return new AuthResponse { Message = "login success" };
        }

        public async Task<AuthResponse> RegisterAsync(RegisterRequest request)
        {
            var user = new User
            {
                Email = request.Email,
                Username = request.Username,
                Role = "user",
                PasswordHash = request.Password // TODO: hash later
            };

            await _userRepository.AddUserAsync(user);

            return new AuthResponse
            { Message = "user registered." };
        }
    }
}
