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
            var stopSWatch = new Stopwatch();
            stopSWatch.Start ();
            await _next(context);
            stopSWatch.Stop ();
            _logger.LogInformation($"{context.Request.Method}");
            _logger.LogInformation($"{context.Request.Path}");
            _logger.LogInformation($"Excution time = {stopSWatch.ElapsedMilliseconds} ms");
            _logger.LogInformation($"{context.Response.StatusCode}");
        }
    }
}
