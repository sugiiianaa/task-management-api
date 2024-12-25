using Microsoft.Extensions.Configuration;
using TaskManagement.Application.Constants;
using TaskManagement.Application.Interfaces;
using TaskManagement.Application.Models.LoginIO;
using TaskManagement.Application.Models.RegisterIO;
using TaskManagement.Domain.Dtos;
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

        public async Task<LoginOuput> LoginUserAsync(LoginInput input)
        {
            var user = await _userRepository.GetUserByEmailAsync(input.Email);

            if (user == null)
            {
                return new LoginOuput
                {
                    IsSuccess = false,
                    Message = ResponseMessages.NotFound
                };
            }

            if (!_passwordService.VerifyPassword(input.Password, user.PasswordHash))
            {
                return new LoginOuput
                {
                    IsSuccess = false,
                    Message = ResponseMessages.BadRequest
                };
            }

            var token = _tokenService.GenerateJwtToken(
                user.Id,
                user.Role);

            return new LoginOuput
            {
                IsSuccess = true,
                Token = token,
            };
        }

        public async Task<RegisterOutput> RegisterUserAsync(RegisterInput input)
        {
            var hashedPassword = _passwordService.HashPassword(input.User.Password);

            var userEntity = new UserDto
            {
                Email = input.User.Email,
                Name = input.User.Name,
                Password = hashedPassword,
                Role = input.User.Role,
            };

            var user = await _userRepository.GetUserByEmailAsync(input.User.Email);

            if (user != null)
            {
                return new RegisterOutput
                {
                    IsSuccess = false,
                    Message = ResponseMessages.BadRequest
                };
            }

            var userId = await _userRepository.AddUserAsync(userEntity);

            return new RegisterOutput
            {
                IsSuccess = true,
                Id = userId,
            };
        }
    }
}
