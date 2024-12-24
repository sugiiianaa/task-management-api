using TaskManagement.Application.DTOs.TaskDtos.CreateTaskDto;
using TaskManagement.Application.DTOs.TaskDtos.GetTaskDto;
using TaskManagement.Application.Interfaces;
using TaskManagement.Domain.Enums;
using TaskManagementAPI.Helper;
using TaskManagementAPI.Models;

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

                var requestDto = new GetAllTaskRequestDto
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

                var requestDto = new CreateTaskRequestDto
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
        }
    }
}
