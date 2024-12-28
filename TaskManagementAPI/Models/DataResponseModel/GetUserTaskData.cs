namespace TaskManagementAPI.Models.DataResponseModel
{
    public class GetUserTaskData
    {
        public required Guid Id { get; set; }
        public required string Title { get; set; }
        public string? Description { get; set; }
        public DateTime ExpectedFinishDate { get; set; }
        public required string TaskStatus { get; set; }
    }
}
