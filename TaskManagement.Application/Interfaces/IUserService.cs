using TaskManagement.Application.Models.LoginIO;
using TaskManagement.Application.Models.RegisterIO;

namespace TaskManagement.Application.Interfaces
{
    public interface IUserService
    {
        Task<RegisterOutput> RegisterUserAsync(RegisterInput requestDto);
        Task<LoginOuput> LoginUserAsync(LoginInput requestDto);
    }
}
