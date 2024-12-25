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
                ExpectedFinishDate = task.ExpectedFinishDate ?? DateTime.UtcNow.AddDays(3),
                TaskOwnerId = task.OwnerId,
                TaskStatus = task.TaskStatus ?? Domain.Enums.UserTaskStatus.Todo,
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

        public async Task<UserTask?> GetUserTaskByIdAsync(Guid taskId)
        {
            return await _appDbContext.UserTasks
                .Where(t => t.Id == taskId)
                .FirstOrDefaultAsync();
        }

        public async Task<Guid?> UpdateUserTaskAsync(UserTaskDto task)
        {
            var userTask = await GetUserTaskByIdAsync(task.Id);

            if (userTask == null) return null;

            // Modify the existing tracked entity directly
            userTask.Title = task.Title ?? userTask.Title;
            userTask.Description = task.Description ?? userTask.Description;
            // Check if the task.ExpectedFinishDate is valid (not null and not the default value)
            if (task.ExpectedFinishDate.HasValue && task.ExpectedFinishDate.Value != default)
            {
                userTask.ExpectedFinishDate = task.ExpectedFinishDate.Value;
            }
            else
            {
                // Keep the existing ExpectedFinishDate if the new value is invalid
                userTask.ExpectedFinishDate = userTask.ExpectedFinishDate;
            }
            userTask.TaskStatus = task.TaskStatus ?? userTask.TaskStatus;

            // Save changes to the existing tracked entity
            await _appDbContext.SaveChangesAsync();

            return userTask.Id;
        }
    }
}
