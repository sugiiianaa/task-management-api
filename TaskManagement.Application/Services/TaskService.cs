using TaskManagement.Application.Interfaces;
using TaskManagement.Application.Models.TaskIO.CreateTaskIO;
using TaskManagement.Application.Models.TaskIO.GetTaskIO;
using TaskManagement.Application.Models.TaskIO.UpdateTaskIO;
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

        public async Task<CreateOutput> CreateTaskAsync(CreateInput input)
        {
            var userTask = new UserTaskDto
            {
                Title = input.Title,
                Description = input.Description,
                ExpectedFinishDate = input.ExpectedFinishDate,
                OwnerId = input.TaskOwnerId,
                TaskStatus = input.TaskStatus
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

        public async Task<GetAllTaskOutput> GetTasksAsync(GetAllTaskInput input)
        {
            var tasks = await _taskRepository.GetAllUserTaskAsync(input.OwnerId);

            return new GetAllTaskOutput
            {
                UserTasks = tasks
            };
        }

        public async Task<UpdateTaskOutput> UpdateTaskAsync(UpdateTaskInput input)
        {
            var Dto = new UserTaskDto
            {
                Id = input.Id,
                Title = input.Title ?? null,
                Description = input.Description ?? null,
                ExpectedFinishDate = input.ExpectedFinishDate ?? null,
                TaskStatus = input.Status ?? null
            };

            var taskId = await _taskRepository.UpdateUserTaskAsync(Dto);

            return new UpdateTaskOutput
            {
                IsSuccess = taskId != null,
                TaskId = taskId ?? null
            };
        }
    }
}
