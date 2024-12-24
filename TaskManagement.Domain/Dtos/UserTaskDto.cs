using TaskManagement.Domain.Enums;

namespace TaskManagement.Domain.Dtos
{
    public class UserTaskDto
    {
        public Guid Id { get; set; }
        public required string Title { get; set; }
        public string? Description { get; set; }
        public DateTime ExpectedFinishDate { get; set; }
        public Guid OwnerId { get; set; }
        public required UserTaskStatus TaskStatus { get; set; }
    }
}
