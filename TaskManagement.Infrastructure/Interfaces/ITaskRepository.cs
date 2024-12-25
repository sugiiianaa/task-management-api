using TaskManagement.Domain.Dtos;

namespace TaskManagement.Infrastructure.Interfaces
{
    public interface ITaskRepository
    {
        Task<bool> CreateUserTaskAsync(UserTaskDto task);
        Task<IList<UserTaskDto>> GetAllUserTaskAsync(Guid ownerId);
    }
}
