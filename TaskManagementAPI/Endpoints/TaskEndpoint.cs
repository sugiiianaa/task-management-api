using System.Security.Claims;
using TaskManagement.Application.Interfaces;

namespace TaskManagementAPI.Endpoints
{
    public static class TaskEndpoint
    {
        public static void MapTaskEndpoint(this WebApplication app)
        {
            app.MapGet("/api/v1/tasks", async (ITaskService taskService, HttpContext httpContext) =>
            {
                var userEmail = httpContext.User?.FindFirst(ClaimTypes.Email)?.Value;

                taskService.GetAllTaskAsync();

                return Results.Ok();
            }).RequireAuthorization();
        }
    }
}
