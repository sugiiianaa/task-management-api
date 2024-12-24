using TaskManagement.Domain.Dtos;

namespace TaskManagement.Application.DTOs.TaskDtos.GetTaskDto
{
    public class GetAllTaskResultDto
    {
        public IList<UserTaskDto>? UserTasks { get; set; }
    };
}
