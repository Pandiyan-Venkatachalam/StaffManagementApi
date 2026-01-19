using System.Net;
using System.Text.Json;

namespace StaffManagementApi.Middleware;

public class GlobalExceptionMiddleware {
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionMiddleware> _logger;

    public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger) {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context) {
        try {
            await _next(context); 
        } catch (Exception ex)
{
    context.Response.StatusCode = 500;
    await context.Response.WriteAsJsonAsync(new
    {
        status = "Error",
        message = ex.Message,
        stackTrace = ex.StackTrace
    });
}
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception) {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        var result = JsonSerializer.Serialize(new { 
            status = "Error", 
            message = "Internal Server Error please check your file." 
        });

        return context.Response.WriteAsync(result);
    }
}