using TaskManagement.Application.Interfaces;
using TaskManagement.Application.Models;
using TaskManagement.Application.Models.Enums;
using TaskManagement.Application.Models.InputModel.UserTask;
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

        public async Task<ApplicationOutputGenericModel<Guid?>> CreateTaskAsync(CreateTaskInput input)
        {
            var userTask = new UserTaskDto
            {
                Title = input.Title,
                Description = input.Description,
                ExpectedFinishDate = input.ExpectedFinishDate,
                OwnerId = input.TaskOwnerId,
                TaskStatus = input.TaskStatus
            };

            var taskId = await _taskRepository.CreateUserTaskAsync(userTask);

            return new ApplicationOutputGenericModel<Guid?>
            {
                Data = taskId,
            };
        }

        public async Task<ApplicationOutputGenericModel<IList<UserTaskDto?>>> GetTasksAsync(Guid input)
        {
            var tasks = await _taskRepository.GetAllUserTaskAsync(input);

            return new ApplicationOutputGenericModel<IList<UserTaskDto?>>
            {
                Data = tasks,
            };
        }

        public async Task<ApplicationOutputGenericModel<Guid?>> UpdateTaskAsync(UpdateTaskInput input)
        {
            var existingTask = await _taskRepository.GetUserTaskByIdAsync(input.TaskId);

            if (existingTask == null)
            {
                return new ApplicationOutputGenericModel<Guid?>
                {
                    ErrorMessage = ApplicationErrorMessage.NotFound
                };
            }

            if (existingTask.TaskOwnerId != input.TaskOwnerId)
            {
                return new ApplicationOutputGenericModel<Guid?>
                {
                    ErrorMessage = ApplicationErrorMessage.NotFound
                };
            }

            var Dto = new UserTaskDto
            {
                Id = input.TaskId,
                Title = input.Title ?? existingTask.Title,
                Description = input.Description ?? existingTask.Description,
                ExpectedFinishDate = input.ExpectedFinishDate ?? existingTask.ExpectedFinishDate,
                TaskStatus = input.TaskStatus ?? existingTask.TaskStatus
            };

            var taskId = await _taskRepository.UpdateUserTaskAsync(Dto);

            return new ApplicationOutputGenericModel<Guid?>
            {
                Data = taskId,
            };
        }

        public async Task<ApplicationOutputGenericModel<Guid?>> DeleteTaskAsync(DeleteTaskInput input)
        {
            var taskId = await _taskRepository.DeleteUserTaskAsync(input.TaskId, input.OwnerTaskId);

            return new ApplicationOutputGenericModel<Guid?>
            {
                Data = taskId,
            };
        }


    }
}
