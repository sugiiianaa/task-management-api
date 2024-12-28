using Microsoft.AspNetCore.Mvc;
using TaskManagement.Application.Interfaces;
using TaskManagement.Application.Models.InputModel.UserTask;
using TaskManagement.Domain.Enums;
using TaskManagementAPI.Enums;
using TaskManagementAPI.Helper;
using TaskManagementAPI.Models.ApiResponseModel;
using TaskManagementAPI.Models.DataResponseModel;
using TaskManagementAPI.Models.RequestModel.UserTask;

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
                    var apiErrorResponseHelper = new ApiErrorResponseHelper();
                    return apiErrorResponseHelper.SendMessage(ApiResponseMessages.Unauthorized);
                }

                var output = await taskService.GetTasksAsync(ownerId.Value);

                if (output.ErrorMessage.HasValue)
                {
                    var apiErrorResponseHelper = new ApiErrorResponseHelper();
                    return apiErrorResponseHelper.SendMessage(output.ErrorMessage.Value);
                }

                var dataResponse = output.Data.Select(task => new GetUserTaskData
                {
                    Id = task.Id,
                    Title = task.Title,
                    Description = task.Description,
                    TaskStatus = TaskHelper.GetStatus(task.TaskStatus),
                    ExpectedFinishDate = task.ExpectedFinishDate
                }).ToList();

                return Results.Ok(new ApiSuccessResponse<List<GetUserTaskData>>
                {
                    Data = dataResponse
                });
            }).RequireAuthorization();

            app.MapPost("/api/v1/create-task", async (ITaskService taskService, CreateUserTaskRequest request, HttpContext httpContext) =>
            {
                var ownerId = httpContext.User.GetOwnerIdClaim();

                if (ownerId == null)
                {
                    var apiErrorResponseHelper = new ApiErrorResponseHelper();
                    return apiErrorResponseHelper.SendMessage(ApiResponseMessages.Unauthorized);
                }

                var input = new CreateTaskInput
                {
                    Title = request.Title,
                    Description = request.Description,
                    ExpectedFinishDate = request.ExpectedFinishDate,
                    TaskStatus = UserTaskStatus.Todo,
                    TaskOwnerId = ownerId.Value,
                };

                var output = await taskService.CreateTaskAsync(input);

                if (output.ErrorMessage.HasValue)
                {
                    var apiErrorResponseHelper = new ApiErrorResponseHelper();
                    return apiErrorResponseHelper.SendMessage(output.ErrorMessage.Value);
                }

                return Results.Created(
                    "/api/v1/resource",
                    new ApiSuccessResponse<Guid>
                    {
                        Data = output.Data.Value
                    });
            }).RequireAuthorization();

            app.MapPatch("/api/v1/update-task", async (ITaskService taskService, UpdateUserTaskRequest request, HttpContext httpContext) =>
            {
                var ownerId = httpContext.User.GetOwnerIdClaim();

                if (ownerId == null)
                {
                    var apiErrorResponseHelper = new ApiErrorResponseHelper();
                    return apiErrorResponseHelper.SendMessage(ApiResponseMessages.Unauthorized);
                }

                UserTaskStatus? userTaskStatusEnum = null;

                if (request.TaskStatus != null)
                {
                    userTaskStatusEnum = TaskHelper.GetStatusFromString(request.TaskStatus);
                    if (userTaskStatusEnum == null)
                    {
                        var apiErrorResponseHelper = new ApiErrorResponseHelper();
                        return apiErrorResponseHelper.SendMessage(ApiResponseMessages.BadRequest);
                    }
                }

                var input = new UpdateTaskInput
                {
                    TaskId = request.TaskId,
                    Title = request.Title,
                    Description = request.Description,
                    TaskOwnerId = ownerId.Value,
                    ExpectedFinishDate = request.ExpectedFinishDate,
                    TaskStatus = userTaskStatusEnum,
                };

                var output = await taskService.UpdateTaskAsync(input);

                if (output.ErrorMessage.HasValue)
                {
                    var apiErrorResponseHelper = new ApiErrorResponseHelper();
                    return apiErrorResponseHelper.SendMessage(output.ErrorMessage.Value);
                }

                return Results.Ok(
                  new ApiSuccessResponse<Guid>
                  {
                      Data = output.Data.Value
                  });

            }).RequireAuthorization();

            app.MapDelete("/api/v1/delete-task/{taskId}", async (ITaskService taskService, [FromRoute] Guid taskId, HttpContext httpContext) =>
            {
                var ownerId = httpContext.User.GetOwnerIdClaim();

                if (ownerId == null)
                {
                    var apiErrorResponseHelper = new ApiErrorResponseHelper();
                    return apiErrorResponseHelper.SendMessage(ApiResponseMessages.Unauthorized);
                }

                var input = new DeleteTaskInput
                {
                    TaskId = taskId,
                    OwnerTaskId = ownerId.Value,
                };

                var output = await taskService.DeleteTaskAsync(input);

                if (output.ErrorMessage.HasValue)
                {
                    var apiErrorResponseHelper = new ApiErrorResponseHelper();
                    return apiErrorResponseHelper.SendMessage(output.ErrorMessage.Value);
                }

                return Results.Ok(
                 new ApiSuccessResponse<Guid>
                 {
                     Data = output.Data.Value
                 });
            }).RequireAuthorization();
        }
    }
}
