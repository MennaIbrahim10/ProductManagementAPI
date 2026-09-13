using System.Diagnostics;

namespace ProductManagement.Middlewares
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestLoggingMiddleware> _logger;

        public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
        {
            _next = next ;
            _logger = logger ;
        }

        public async Task Invoke (HttpContext context)
        {
            var stopwatch = Stopwatch.StartNew();
            await _next(context);
            stopwatch.Stop ();
            _logger.LogInformation($"{context.Request.Method}");
            _logger.LogInformation($"{context.Request.Path}");
            _logger.LogInformation($"Excution time = {stopwatch.ElapsedMilliseconds} ms");
            _logger.LogInformation($"{context.Response.StatusCode}");
        }
    }
}
