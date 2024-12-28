namespace TaskManagement.Application.Models.InputModel.Auth
{
    public class RegisterUserInput
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required string ReTypePassword { get; set; }
        public required string Name { get; set; }
    }
}
