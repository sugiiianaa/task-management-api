namespace TaskManagementAPI.Models.RequestModel.UserTask
{
    public class CreateUserTaskRequest
    {
        public required string Title { get; set; }
        public string? Description { get; set; }
        public DateTime ExpectedFinishDate { get; set; }
    }
}
