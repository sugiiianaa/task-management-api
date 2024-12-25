using TaskManagement.Domain.Enums;

namespace TaskManagement.Domain.Dtos
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required UserRoles Role { get; set; }
    }
}
