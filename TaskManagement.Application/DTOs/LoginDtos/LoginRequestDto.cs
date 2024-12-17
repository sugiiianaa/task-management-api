namespace TaskManagement.Application.DTOs.LoginDtos
{
    public class LoginRequestDto
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}
