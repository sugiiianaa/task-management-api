using TaskManagement.Domain.Enums;

namespace TaskManagement.Application.Models.InputModel.UserTask
{
    public class UpdateTaskInput
    {
        public required Guid TaskId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public required Guid TaskOwnerId { get; set; }
        public DateTime? ExpectedFinishDate { get; set; }
        public UserTaskStatus? TaskStatus { get; set; }
    }
}
