    using System.Net;
    using System.Text.Json;
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
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

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var errorResponse = JsonSerializer.Serialize(new
            {
                status = "error",
                error = new
                {
                    code = "InternalServerError",
                    message = "An internal server error has occurred.",
                    detailed = exception.Message 
                }
            });

            return context.Response.WriteAsync(errorResponse);
        }

    }
