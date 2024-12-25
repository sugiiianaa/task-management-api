using TaskManagement.Domain.Dtos;

namespace TaskManagement.Application.Models.RegisterIO
{
    public class RegisterInput
    {
        public required UserDto User { get; set; }
    }
}
