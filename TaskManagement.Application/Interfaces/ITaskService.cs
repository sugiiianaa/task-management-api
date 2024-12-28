using TaskManagement.Application.Models;
using TaskManagement.Application.Models.InputModel.UserTask;
using TaskManagement.Domain.Dtos;

namespace TaskManagement.Application.Interfaces
{
    public interface ITaskService
    {
        Task<ApplicationOutputGenericModel<Guid?>> CreateTaskAsync(CreateTaskInput input);
        Task<ApplicationOutputGenericModel<IList<UserTaskDto?>>> GetTasksAsync(Guid input);
        Task<ApplicationOutputGenericModel<Guid?>> UpdateTaskAsync(UpdateTaskInput input);
        Task<ApplicationOutputGenericModel<Guid?>> DeleteTaskAsync(DeleteTaskInput input);
    }
}
