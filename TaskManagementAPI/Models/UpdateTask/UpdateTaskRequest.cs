namespace TaskManagementAPI.Models.UpdateTask
{
    public class UpdateTaskRequest
    {
        public Guid TaskId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime ExpectedFinishDate { get; set; }
        public string? TaskStatus { get; set; }
    }
}
