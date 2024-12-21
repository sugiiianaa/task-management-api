using TaskManagement.Domain.Entities;

namespace TaskManagement.Infrastructure.Interfaces
{
    public interface IUserRepository
    {
        Task AddUserAsync(User user);
        Task<User?> GetUserByEmailAsync(string email);
    }
}
