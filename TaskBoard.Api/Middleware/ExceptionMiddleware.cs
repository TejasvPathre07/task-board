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
                context.Response.ContentType = "application/json";

                var statusCode = (int)HttpStatusCode.InternalServerError;
                var message = "Something went wrong";

                // simple check for duplicate
                if (ex.Message.Contains("already exists"))
                {
                    statusCode = (int)HttpStatusCode.Conflict;
                    message = ex.Message;
                }

                context.Response.StatusCode = statusCode;

                var result = JsonSerializer.Serialize(new
                {
                    success = false,
                    message = message
                });

                await context.Response.WriteAsync(result);
            }
        }
    }
}
