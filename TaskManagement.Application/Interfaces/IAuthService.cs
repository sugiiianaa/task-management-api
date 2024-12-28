using TaskManagement.Application.Models;
using TaskManagement.Application.Models.InputModel.Auth;
using TaskManagement.Domain.Enums;

namespace TaskManagement.Application.Interfaces
{
    public interface IAuthService
    {
        // Authentication Service
        Task<ApplicationOutputGenericModel<Guid?>> RegisterUserAsync(RegisterUserInput input);
        Task<ApplicationOutputGenericModel<string?>> LoginUserAsync(LoginUserInput input);

        // Password Services
        byte[] GenerateSalt();
        string HashPassword(string password);
        bool VerifyPassword(string providedPassword, string storedHashedPassword);

        // Token Service
        string GenerateJwtToken(Guid ownerId, UserRoles role);
    }
}
