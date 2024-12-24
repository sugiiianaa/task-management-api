using Microsoft.Extensions.Configuration;
using TaskManagement.Application.Constants;
using TaskManagement.Application.DTOs.LoginDtos;
using TaskManagement.Application.DTOs.RegisterDtos;
using TaskManagement.Application.Enums;
using TaskManagement.Application.Interfaces;
using TaskManagement.Domain.Entities;
using TaskManagement.Infrastructure.Interfaces;

namespace TaskManagement.Application.Services
{
    public class UserService(
        IUserRepository userRepository,
        IConfiguration configuration,
        IPasswordService passwordService,
        ITokenService tokenService) : IUserService
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IConfiguration _configuration = configuration;
        private readonly IPasswordService _passwordService = passwordService;
        private readonly ITokenService _tokenService = tokenService;

        public async Task<LoginResponseDto> LoginUserAsync(LoginRequestDto requestDto)
        {
            var user = await _userRepository.GetUserByEmailAsync(requestDto.Email);

            if (user == null)
            {
                return new LoginResponseDto
                {
                    IsSuccess = false,
                    Message = ResponseMessages.NotFound
                };
            }

            if (!_passwordService.VerifyPassword(requestDto.Password, user.PasswordHash))
            {
                return new LoginResponseDto
                {
                    IsSuccess = false,
                    Message = ResponseMessages.BadRequest
                };
            }

            var secret = _configuration["Jwt:Secret"];

            var token = _tokenService.GenerateJwtToken(
                user.Id,
                user.Role);

            return new LoginResponseDto
            {
                IsSuccess = true,
                Token = token,
            };
        }

        public async Task<RegisterResponseDto> RegisterUserAsync(RegisterRequestDto requestDto)
        {
            var hashedPassword = _passwordService.HashPassword(requestDto.Password);

            var userEntity = new User
            {
                Email = requestDto.Email,
                Name = requestDto.Name,
                PasswordHash = hashedPassword,
                Role = RoleHelper.GetRoleName(Role.User),
            };

            var user = await _userRepository.GetUserByEmailAsync(requestDto.Email);

            if (user != null)
            {
                return new RegisterResponseDto
                {
                    IsSuccess = false,
                    Message = ResponseMessages.BadRequest
                };
            }

            await _userRepository.AddUserAsync(userEntity);

            return new RegisterResponseDto
            {
                IsSuccess = true
            };
        }
    }
}
