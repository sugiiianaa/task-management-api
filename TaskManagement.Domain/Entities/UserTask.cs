namespace TaskManagement.Domain.Entities
{
    public class UserTask
    {
        public Guid Id { get; set; }
        public required string Title { get; set; }
        public string? Description { get; set; }
        public DateTime ExpectedFinishDate { get; set; }

        // Renamed for clarity
        public Guid AssignedUserId { get; set; }
        public required string TaskStatus { get; set; }

        // Navigation property for related subtasks
        public IList<SubTask> Subtasks { get; set; } = new List<SubTask>();

        // Navigation property for the assigned user
        public User? AssignedUser { get; set; }
    }
}
