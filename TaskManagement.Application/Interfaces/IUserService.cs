using TaskManagement.Application.DTOs.LoginDtos;
using TaskManagement.Application.DTOs.RegisterDtos;

namespace TaskManagement.Application.Interfaces
{
    public interface IUserService
    {
        Task<RegisterResponseDto> RegisterUserAsync(RegisterRequestDto requestDto);
        Task<LoginResponseDto> LoginUserAsync(LoginRequestDto requestDto);
    }
}
