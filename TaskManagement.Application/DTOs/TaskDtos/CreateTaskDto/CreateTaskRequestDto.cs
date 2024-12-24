using TaskManagement.Domain.Enums;

namespace TaskManagement.Application.DTOs.TaskDtos.CreateTaskDto
{
    public class CreateTaskRequestDto
    {
        public required string Title { get; set; }
        public string? Description { get; set; }
        public DateTime ExpectedFinishDate { get; set; }
        public required UserTaskStatus TaskStatus { get; set; }
        public Guid TaskOwnerId { get; set; }
    }
}
