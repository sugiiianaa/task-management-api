namespace TaskManagementAPI.Models.Register
{
    public class RegisterResponse
    {
        public Guid Id { get; set; }
        public required string Email { get; set; }
    }
}
