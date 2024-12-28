namespace TaskManagementAPI.Models.RequestModel.UserTask
{
    public class UpdateUserTaskRequest
    {
        public required Guid TaskId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime? ExpectedFinishDate { get; set; }
        public string? TaskStatus { get; set; }
    }
}
