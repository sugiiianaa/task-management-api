using TaskManagement.Domain.Enums;

namespace TaskManagement.Domain.Entities
{
    public class UserTask
    {
        public Guid Id { get; set; }
        public required string Title { get; set; }
        public string? Description { get; set; }
        public DateTime ExpectedFinishDate { get; set; }

        public Guid TaskOwnerId { get; set; }
        public User? TaskOwner { get; set; }
        public UserTaskStatus TaskStatus { get; set; }

        public IList<SubTask> Subtasks { get; set; } = new List<SubTask>();
    }
}
