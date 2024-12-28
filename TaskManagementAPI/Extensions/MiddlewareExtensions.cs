using AspNetCoreRateLimit;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using TaskManagement.Infrastructure.Persistence;
using TaskManagementAPI.Endpoints;
using TaskManagementAPI.Enums;
using TaskManagementAPI.Middleware;
using TaskManagementAPI.Models.ApiResponseModel;

namespace TaskManagementAPI.Extensions
{
    public static class MiddlewareExtensions
    {
        public static void ConfigureAppMiddleware(this WebApplication app)
        {
            // Swagger setup
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "TaskManagement API v1");
                    c.RoutePrefix = string.Empty;
                });

                // Auto-migrate database
                using var scope = app.Services.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                dbContext.Database.Migrate();
            }

            // Global error handling
            app.UseExceptionHandler(errorApp =>
            {
                errorApp.Run(async context =>
                {
                    var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;

                    var response = new ApiErrorResponse
                    {
                        Message = app.Environment.IsDevelopment() ? exception.Message : ApiResponseMessageHelper.GetMessage(ApiResponseMessages.InternalServerError)
                    };

                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsJsonAsync(response);
                });
            });

            // General Middleware
            app.UseHttpsRedirection();
            app.UseResponseCompression();
            app.UseIpRateLimiting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseMiddleware<BadRequestHandlingMiddleware>();
            app.UseMiddleware<LoggingMiddleware>();

            // Map Endpoints
            app.MapAuthEndpoints();
            app.MapTaskEndpoint();
        }
    }
}
