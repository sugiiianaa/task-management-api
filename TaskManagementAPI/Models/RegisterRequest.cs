namespace TaskManagementAPI.Models
{
    public class RegisterRequest
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required string ReTypePassword { get; set; }
        public required string Name { get; set; }
    }
}
