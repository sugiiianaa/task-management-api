using TaskManagement.Application.Models.TaskIO.CreateTaskIO;
using TaskManagement.Application.Models.TaskIO.GetTaskIO;
using TaskManagement.Application.Models.TaskIO.UpdateTaskIO;

namespace TaskManagement.Application.Interfaces
{
    public interface ITaskService
    {
        Task<GetAllTaskOutput> GetTasksAsync(GetAllTaskInput input);
        Task<CreateOutput> CreateTaskAsync(CreateInput input);
        Task<UpdateTaskOutput> UpdateTaskAsync(UpdateTaskInput input);
    }
}
