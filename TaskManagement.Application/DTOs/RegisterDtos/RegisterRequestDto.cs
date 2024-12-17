using Microsoft.Extensions.Primitives;

namespace TaskManagement.Application.DTOs.RegisterDtos
{
    public class RegisterRequestDto
    {
        public required string Email { get; set; }
        public required string Name { get; set; }
        public required string Password { get; set; }
    }
}
