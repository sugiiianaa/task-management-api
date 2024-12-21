using AspNetCoreRateLimit;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using TaskManagement.Infrastructure.Persistence;
using TaskManagementAPI.Endpoints;
using TaskManagementAPI.Middleware;

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

            // Error handling
            app.UseExceptionHandler(errorApp =>
            {
                errorApp.Run(async context =>
                {
                    context.Response.ContentType = "application/json";
                    var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;

                    var errorResponse = new
                    {
                        StatusCode = exception is BadHttpRequestException
                            ? StatusCodes.Status400BadRequest
                            : StatusCodes.Status500InternalServerError,
                        Message = exception is BadHttpRequestException
                            ? "Invalid request format."
                            : "An unexpected error occurred.",
                        Details = app.Environment.IsDevelopment() ? exception?.StackTrace : null
                    };

                    context.Response.StatusCode = errorResponse.StatusCode;
                    await context.Response.WriteAsJsonAsync(errorResponse);
                });
            });

            // General Middleware
            app.UseHttpsRedirection();
            app.UseResponseCompression();
            app.UseIpRateLimiting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseMiddleware<LoggingMiddleware>();

            // Map Endpoints
            app.MapAuthEndpoints();
            app.MapTaskEndpoint();
        }
    }
}
