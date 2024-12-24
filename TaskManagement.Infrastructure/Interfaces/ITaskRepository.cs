using TaskManagement.Domain.Dtos;

namespace TaskManagement.Infrastructure.Interfaces
{
    public interface ITaskRepository
    {
        Task<IList<UserTaskDto>> GetAllUserTaskAsync(Guid ownerId);
        Task<bool> CreateUserTaskAsync(UserTaskDto task);
    }
}
