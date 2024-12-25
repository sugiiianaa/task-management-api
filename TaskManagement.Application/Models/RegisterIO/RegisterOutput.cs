namespace TaskManagement.Application.Models.RegisterIO
{
    public class RegisterOutput
    {
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
        public Guid Id { get; set; }
    }
}
