using TaskManagementAPI.Enums;
using TaskManagementAPI.Models.ApiResponseModel;

namespace TaskManagementAPI.Middleware
{
    public class BadRequestHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public BadRequestHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (BadHttpRequestException)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsJsonAsync(new ApiErrorResponse
                {
                    Message = ApiResponseMessageHelper.GetMessage(ApiResponseMessages.BadRequest),
                });
            }
        }
    }
}
