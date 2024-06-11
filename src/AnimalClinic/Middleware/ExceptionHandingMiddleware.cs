using System.Net;
namespace AnimalClinic.Middleware;

public class ExceptionHandingMiddleware
{
    private readonly RequestDelegate _next; // It holds a reference to the next middleware component in the pipeline
    private readonly ILogger<ExceptionHandingMiddleware> _logger;

    public ExceptionHandingMiddleware(RequestDelegate next, ILogger<ExceptionHandingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext); // Calls the next middleware in the pipeline
        }
        catch (Exception ex)
        {
            _logger.LogError($"Something went wrong: {ex}");
            await HandleExceptionAsync(httpContext, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        context.Response.ContentType = "application/json"; // ContentType property specifies the media type (MIME type)
            // of the HTTP response. It tells the client (like a web browser or mobile app) how to interpret the content
            // of the respond. For our case, it means the body of the response will be in JSON format.
            // Other common types are 'text/html' and 'text/plain'.
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        var response = new
        {
            StatusCode = context.Response.StatusCode,
            Message = "Internal Server Error. An unexpected error occured."
        };

        return context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(response));
    }
}