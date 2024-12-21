namespace TaskManagement.Domain.Entities
{
    public class UserProfile
    {
        public Guid Id { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }

        // Reference to User class
        public required User User { get; set; }
    }
}
