using TaskManagement.Application.Interfaces;
using TaskManagement.Application.Models.TaskIO.CreateTaskIO;
using TaskManagement.Application.Models.TaskIO.GetTaskIO;
using TaskManagement.Application.Models.TaskIO.UpdateTaskIO;
using TaskManagement.Domain.Enums;
using TaskManagementAPI.Helper;
using TaskManagementAPI.Models;
using TaskManagementAPI.Models.UpdateTask;

namespace TaskManagementAPI.Endpoints
{
    public static class TaskEndpoint
    {
        public static void MapTaskEndpoint(this WebApplication app)
        {
            app.MapGet("/api/v1/tasks", async (ITaskService taskService, HttpContext httpContext) =>
            {
                var ownerId = httpContext.User.GetOwnerIdClaim();

                if (ownerId == null)
                {
                    return Results.BadRequest("Invalid or missing owner ID in token.");
                }

                var requestDto = new GetAllTaskInput
                {
                    OwnerId = ownerId.Value
                };

                var result = await taskService.GetTasksAsync(requestDto);

                return Results.Ok(result);
            }).RequireAuthorization();

            app.MapPost("/api/v1/create-task", async (ITaskService taskService, CreateUserTaskRequest request, HttpContext httpContext) =>
            {
                var ownerId = httpContext.User.GetOwnerIdClaim();

                if (ownerId == null)
                {
                    return Results.BadRequest("Invalid or missing owner ID in token.");
                }

                var taskStatus = TaskHelper.GetStatusFromString(request.TaskStatus);

                if (taskStatus == null)
                {
                    return Results.BadRequest("Invalid task status value");
                }

                var requestDto = new CreateInput
                {
                    Title = request.Title,
                    Description = request.Description,
                    ExpectedFinishDate = request.ExpectedFinishDate ?? DateTime.UtcNow,
                    TaskStatus = taskStatus.Value,
                    TaskOwnerId = ownerId.Value,
                };

                var result = await taskService.CreateTaskAsync(requestDto);
                return Results.Ok(result);
            }).RequireAuthorization();

            app.MapPut("/api/v1/update-task", async (ITaskService taskService, UpdateTaskRequest request, HttpContext httpcontext) =>
            {
                var ownerId = httpcontext.User.GetOwnerIdClaim();

                if (ownerId == null) return Results.BadRequest();

                UserTaskStatus? taskStatus = null;

                if (request.TaskStatus != null)
                {
                    taskStatus = TaskHelper.GetStatusFromString(request.TaskStatus);

                    if (taskStatus == null)
                    {
                        return Results.BadRequest("Invalid task status value");
                    }
                }

                var input = new UpdateTaskInput
                {
                    Id = request.TaskId,
                    Title = request.Title,
                    Description = request.Description,
                    ExpectedFinishDate = request.ExpectedFinishDate,
                    Status = taskStatus,
                };

                var output = await taskService.UpdateTaskAsync(input);

                if (output.IsSuccess == false)
                {
                    return Results.InternalServerError();
                }

                return Results.Ok(output);
            }).RequireAuthorization();
        }
    }
}
