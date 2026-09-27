using System.Diagnostics;

namespace Apps.Middlewares
{
    public class RequestLoggingMiddleware
    {
        private readonly ILogger<RequestLoggingMiddleware> _logger;
        private RequestDelegate _next;
        public RequestLoggingMiddleware(ILogger<RequestLoggingMiddleware> logger , RequestDelegate next)
        {
            _logger = logger;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var stopWatch = Stopwatch.StartNew();
            var requestId = context.TraceIdentifier;

            context.Response.Headers["X-Request-Id"] = requestId;

            _logger.LogInformation(
                "Request started: {Method} {Path} | ID: {RequestId}",
                context.Request.Method,
                context.Request.Path,
                requestId
                );

            try
            {
                await _next(context);
            }
            finally { 
                
                stopWatch.Stop();

                _logger.LogInformation(
                    "Request finished: {StatusCode} in {ElapsedMs} ms | ID: {RequestId}",
                    context.Response.StatusCode,
                    stopWatch.ElapsedMilliseconds,
                    requestId);
            }
        }
    }
}
