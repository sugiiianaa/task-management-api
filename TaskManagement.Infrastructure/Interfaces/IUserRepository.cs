using TaskManagement.Domain.Dtos;
using TaskManagement.Domain.Entities;

namespace TaskManagement.Infrastructure.Interfaces
{
    public interface IUserRepository
    {
        Task<Guid> AddUserAsync(UserDto user);
        Task<User?> GetUserByEmailAsync(string email);
    }
}
