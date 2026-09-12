using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace ProductManagement.Filters
{
    public class ExecutionTimeFilter : IAsyncActionFilter
    {
        private readonly ILogger<ExecutionTimeFilter> _logger;

        public ExecutionTimeFilter(ILogger<ExecutionTimeFilter> logger)
        {
            _logger = logger;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            await next();
            stopwatch.Stop();
            _logger.LogInformation($"{context.Controller}.{context.ActionDescriptor.DisplayName}\nExecution Time: {stopwatch.ElapsedMilliseconds}ms");
        }

    }
}
