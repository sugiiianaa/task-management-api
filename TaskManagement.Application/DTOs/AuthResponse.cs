namespace TaskManagement.Application.DTOs
{
    public class AuthResponse
    {
        public bool IsSuccess { get; set; }
        public required string Message {  get; set; }
        public string? Token { get; set; }
    }
}
