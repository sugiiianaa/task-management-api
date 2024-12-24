namespace TaskManagement.Application.Interfaces
{
    public interface ITokenService
    {
        string GenerateJwtToken(Guid ownerId, string role);
    }
}
