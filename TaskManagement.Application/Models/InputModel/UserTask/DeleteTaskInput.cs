namespace TaskManagement.Application.Models.InputModel.UserTask
{
    public class DeleteTaskInput
    {
        public required Guid TaskId { get; set; }
        public required Guid OwnerTaskId { get; set; }
    }
}
