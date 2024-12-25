using TaskManagement.Application.Interfaces;
using TaskManagement.Application.Models.TaskIO.CreateTaskIO;
using TaskManagement.Application.Models.TaskIO.GetTaskIO;
using TaskManagement.Domain.Dtos;
using TaskManagement.Infrastructure.Interfaces;

namespace TaskManagement.Application.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;

        public TaskService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<CreateOutput> CreateTaskAsync(CreateInput request)
        {
            var userTask = new UserTaskDto
            {
                Title = request.Title,
                Description = request.Description,
                ExpectedFinishDate = request.ExpectedFinishDate,
                OwnerId = request.TaskOwnerId,
                TaskStatus = request.TaskStatus
            };

            try
            {
                var IsCreated = await _taskRepository.CreateUserTaskAsync(userTask);
                return new CreateOutput
                {
                    IsSuccess = IsCreated,
                };
            }
            catch (Exception ex)
            {
                return new CreateOutput
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<GetAllTaskOutput> GetTasksAsync(GetAllTaskInput request)
        {
            var tasks = await _taskRepository.GetAllUserTaskAsync(request.OwnerId);

            return new GetAllTaskOutput
            {
                UserTasks = tasks
            };
        }
    }
}
