using System.Diagnostics.Metrics;

namespace ProductManagement.Middlewares
{
    public class LimitingMiddleware
    {
        private readonly RequestDelegate _next;
        private static int counter = 0;
        private static DateTime LastRequestTime = DateTime.UtcNow;
        public LimitingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke (HttpContext context)
        {
            counter++;
            if (DateTime.UtcNow.Subtract(LastRequestTime).TotalSeconds > 10)
            {
                counter = 1;
                LastRequestTime = DateTime.UtcNow;
                await _next(context);
            }
            else
            {
                if(counter > 5)
                {
                    LastRequestTime = DateTime.UtcNow;
                    context.Response.StatusCode = 429;
                    await context.Response.WriteAsync("Limit exceeded");
                }
                else
                {
                    LastRequestTime = DateTime.UtcNow;
                    await _next(context);
                }
            }
        }
    }
}
