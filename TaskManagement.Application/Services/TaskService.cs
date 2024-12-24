using TaskManagement.Application.DTOs.TaskDtos.CreateTaskDto;
using TaskManagement.Application.DTOs.TaskDtos.GetTaskDto;
using TaskManagement.Application.Interfaces;
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

        public async Task<CreateTaskResultDto> CreateTaskAsync(CreateTaskRequestDto request)
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
                return new CreateTaskResultDto
                {
                    IsSuccess = IsCreated,
                };
            }
            catch (Exception ex)
            {
                return new CreateTaskResultDto
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<GetAllTaskResultDto> GetTasksAsync(GetAllTaskRequestDto request)
        {
            var tasks = await _taskRepository.GetAllUserTaskAsync(request.OwnerId);

            return new GetAllTaskResultDto
            {
                UserTasks = tasks
            };
        }
    }
}
