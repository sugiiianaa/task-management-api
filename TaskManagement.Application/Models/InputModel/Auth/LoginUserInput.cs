namespace TaskManagement.Application.Models.InputModel.Auth
{
    public class LoginUserInput
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}
