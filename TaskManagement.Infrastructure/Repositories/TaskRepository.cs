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

        public async Task<Guid> CreateUserTaskAsync(UserTaskDto task)
        {
            var userTask = new UserTask
            {
                Title = task.Title,
                Description = task.Description,
                ExpectedFinishDate = task.ExpectedFinishDate,
                TaskOwnerId = task.OwnerId,
                TaskStatus = task.TaskStatus,
            };

            var userTaskRecord = _appDbContext.UserTasks.Add(userTask);

            await _appDbContext.SaveChangesAsync();

            return userTaskRecord.Entity.Id;
        }

        public async Task<Guid?> DeleteUserTaskAsync(Guid id, Guid userId)
        {
            var rowUpdated = await _appDbContext.UserTasks.Where(t => t.Id == id && t.TaskOwnerId == userId).ExecuteDeleteAsync();
            if (rowUpdated < 1) return null;
            return id;
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

        public async Task<UserTask?> GetUserTaskByIdAsync(Guid taskId)
        {
            return await _appDbContext.UserTasks
                .Where(t => t.Id == taskId)
                .FirstOrDefaultAsync();
        }

        public async Task<Guid?> UpdateUserTaskAsync(UserTaskDto task)
        {
            var taskRecord = await _appDbContext.UserTasks.SingleOrDefaultAsync(t => t.Id == task.Id);

            taskRecord.Title = task.Title;
            taskRecord.Description = task.Description;
            taskRecord.ExpectedFinishDate = task.ExpectedFinishDate;
            taskRecord.TaskStatus = task.TaskStatus;

            // Save changes to the existing tracked entity
            await _appDbContext.SaveChangesAsync();

            return taskRecord.Id;
        }
    }
}
