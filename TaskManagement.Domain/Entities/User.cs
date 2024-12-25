using TaskManagement.Domain.Enums;

namespace TaskManagement.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string PasswordHash { get; set; }
        public required UserRoles Role { get; set; }

        public IList<UserTask> Tasks { get; set; } = new List<UserTask>();
        public UserProfile? Profile { get; set; }
    }
}
