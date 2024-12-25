using TaskManagement.Application.Models.TaskIO.CreateTaskIO;
using TaskManagement.Application.Models.TaskIO.GetTaskIO;

namespace TaskManagement.Application.Interfaces
{
    public interface ITaskService
    {
        Task<GetAllTaskOutput> GetTasksAsync(GetAllTaskInput request);
        Task<CreateOutput> CreateTaskAsync(CreateInput request);
    }
}
