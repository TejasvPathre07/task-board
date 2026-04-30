using System.Net;
using System.Text.Json;

namespace TaskBoard.Api.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var response = context.Response;
            response.ContentType = "application/json";

            var statusCode = HttpStatusCode.InternalServerError;
            var message = "Something went wrong";

            // Custom handling
            if (ex.Message.Contains("already exists"))
            {
                statusCode = HttpStatusCode.Conflict; // 409
                message = ex.Message;
            }

            response.StatusCode = (int)statusCode;

            var result = JsonSerializer.Serialize(new
            {
                success = false,
                message = message
            });

            return response.WriteAsync(result);
        }
    }
}