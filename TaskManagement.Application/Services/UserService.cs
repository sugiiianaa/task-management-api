using TaskManagement.Application.Constants;
using TaskManagement.Application.DTOs.LoginDtos;
using TaskManagement.Application.DTOs.RegisterDtos;
using TaskManagement.Application.Interfaces;
using TaskManagement.Domain.Entities;
using TaskManagement.Infrastructure.Interfaces;

namespace TaskManagement.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;

        public UserService(
            IUserRepository userRepository, 
            IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }

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


            if (!_passwordHasher.VerifyPassword(user.PasswordHash, requestDto.Password))
            {
                return new LoginResponseDto
                {
                    IsSuccess = false,
                    Message = ResponseMessages.BadRequest
                };
            }

            // TOOD : implement token generate code later
            var token = "token";

            return new LoginResponseDto
            {
                IsSuccess = true,
                Token = token,
            };
        }

        public async Task<RegisterResponseDto> RegisterUserAsync(RegisterRequestDto requestDto)
        {
            var hashedPassword = _passwordHasher.HashPassword(requestDto.Password);
            
            var userEntity = new User
            {
                Email = requestDto.Email,
                Name = requestDto.Name,
                PasswordHash = hashedPassword,
                Role = "User"
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

            return new RegisterResponseDto { 
                IsSuccess = true 
            };
        }
    }
}
