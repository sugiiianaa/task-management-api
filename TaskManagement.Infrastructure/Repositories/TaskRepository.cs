using Microsoft.EntityFrameworkCore;
using TaskManagement.Domain.Dtos;
using TaskManagement.Domain.Entities;
using TaskManagement.Infrastructure.Interfaces;
using TaskManagement.Infrastructure.Persistence;

namespace TaskManagement.Infrastructure.Repositories
{
    public class TaskRepository(AppDbContext appDbContext) : ITaskRepository
    {
        private readonly AppDbContext _appDbContext = appDbContext;


        public async Task<bool> CreateUserTaskAsync(UserTaskDto task)
        {
            var userTask = new UserTask
            {
                Title = task.Title,
                Description = task.Description,
                ExpectedFinishDate = task.ExpectedFinishDate,
                TaskOwnerId = task.OwnerId,
                TaskStatus = task.TaskStatus,
            };
            _appDbContext.UserTasks.Add(userTask);
            await _appDbContext.SaveChangesAsync();
            return true;
        }

        public async Task<IList<UserTaskDto>> GetAllUserTaskAsync(Guid ownerId)
        {
            var tasks = await _appDbContext.UserTasks
                .Where(ut => EF.Functions.Like(ut.TaskOwnerId.ToString(), ownerId.ToString()))
                .ToListAsync();

            return tasks.Select(t => new UserTaskDto
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                ExpectedFinishDate = t.ExpectedFinishDate,
                OwnerId = ownerId,
                TaskStatus = t.TaskStatus
            }).ToList();
        }
    }
}
