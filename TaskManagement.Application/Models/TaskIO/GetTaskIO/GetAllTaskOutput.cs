using TaskManagement.Domain.Dtos;

namespace TaskManagement.Application.Models.TaskIO.GetTaskIO
{
    public class GetAllTaskOutput
    {
        public IList<UserTaskDto>? UserTasks { get; set; }
    };
}
