using Microsoft.EntityFrameworkCore;
using TaskManagement.Domain.Dtos;
using TaskManagement.Domain.Entities;
using TaskManagement.Infrastructure.Interfaces;
using TaskManagement.Infrastructure.Persistence;

namespace TaskManagement.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _appDbContext;

        public UserRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Guid> AddUserAsync(UserDto user)
        {
            var userEntity = new User
            {
                Name = user.Name,
                Email = user.Email,
                PasswordHash = user.Password,
                Role = user.Role,
            };

            _appDbContext.Users.Add(userEntity);

            await _appDbContext.SaveChangesAsync();

            return userEntity.Id;
        }

        public Task<User?> GetUserByEmailAsync(string email)
        {
            return _appDbContext.Users
                .FirstOrDefaultAsync(u => EF.Functions.Like(u.Email, email));
        }

    }
}
