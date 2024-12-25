using TaskManagement.Domain.Enums;

namespace TaskManagement.Application.Interfaces
{
    public interface ITokenService
    {
        string GenerateJwtToken(Guid ownerId, UserRoles role);
    }
}
