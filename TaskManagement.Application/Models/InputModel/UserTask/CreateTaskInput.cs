using TaskManagement.Domain.Enums;

namespace TaskManagement.Application.Models.InputModel.UserTask
{
    public class CreateTaskInput
    {
        public required string Title { get; set; }
        public string? Description { get; set; }
        public DateTime ExpectedFinishDate { get; set; }
        public Guid TaskOwnerId { get; set; }
        public UserTaskStatus TaskStatus { get; set; }
    }
}
