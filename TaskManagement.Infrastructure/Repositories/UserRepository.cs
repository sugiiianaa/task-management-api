using Microsoft.EntityFrameworkCore;
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

        public async Task AddUserAsync(User user)
        {
            _appDbContext.Users.Add(user);
            await _appDbContext.SaveChangesAsync();
        }

        public Task<User?> GetUserByEmailAsync(string email)
        {
            return _appDbContext.Users
                .FirstOrDefaultAsync(u => EF.Functions.Like(u.Email, email));
        }
    }
}
