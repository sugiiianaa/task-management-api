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

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
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

            // TODO : implement hashing logic later
            var passwordHash = requestDto.Password;

            if (passwordHash != user.PasswordHash)
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
            var userEntity = new User
            {
                Email = requestDto.Email,
                Name = requestDto.Name,
                PasswordHash = requestDto.Password, //hash later
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
