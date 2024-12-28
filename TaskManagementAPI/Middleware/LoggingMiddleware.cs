namespace TaskManagementAPI.Middleware
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<LoggingMiddleware> _logger;

        public LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                // Log the incoming request
                _logger.LogInformation("Handling request: {RequestMethod} {RequestPath}", context.Request.Method, context.Request.Path);

                // Continue to the next middleware
                await _next(context);
            }
            catch (Exception ex)
            {
                // Log the exception and continue
                _logger.LogError(ex, "Unhandled exception occurred while processing the request.");

                throw;
            }
        }
    }
}
