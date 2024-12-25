using TaskManagement.Domain.Enums;

namespace TaskManagement.Application.Models.TaskIO.UpdateTaskIO
{
    public class UpdateTaskInput
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime? ExpectedFinishDate { get; set; }
        public UserTaskStatus? Status { get; set; }
    }
}
