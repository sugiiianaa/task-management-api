namespace TaskManagement.Domain.Entities
{
    public class SubTask
    {
        public Guid Id { get; set; }
        public required string Title { get; set; }
        public required bool IsCompleted { get; set; }

        // Foreign key to Task
        public Guid TaskId { get; set; }
        public UserTask Task { get; set; }  // Navigation property to Task
    }
}
