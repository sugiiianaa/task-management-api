using TaskManagement.Domain.Dtos;
using TaskManagement.Domain.Entities;

namespace TaskManagement.Infrastructure.Interfaces
{
    public interface ITaskRepository
    {
        Task<bool> CreateUserTaskAsync(UserTaskDto task);
        Task<IList<UserTaskDto>> GetAllUserTaskAsync(Guid ownerId);
        Task<UserTask?> GetUserTaskByIdAsync(Guid id);
        Task<Guid?> UpdateUserTaskAsync(UserTaskDto task);
    }
}
