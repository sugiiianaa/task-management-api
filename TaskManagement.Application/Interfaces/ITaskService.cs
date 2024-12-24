using TaskManagement.Application.DTOs.TaskDtos.CreateTaskDto;
using TaskManagement.Application.DTOs.TaskDtos.GetTaskDto;

namespace TaskManagement.Application.Interfaces
{
    public interface ITaskService
    {
        Task<GetAllTaskResultDto> GetTasksAsync(GetAllTaskRequestDto request);
        Task<CreateTaskResultDto> CreateTaskAsync(CreateTaskRequestDto request);
    }
}
