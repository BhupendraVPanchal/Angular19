using Microsoft.AspNetCore.Http;
using System.Net;
using System.Text.Json;

namespace Helper_CL.middleware
{
    public class ErrorDetails
    {
        public int status_code { get; set; }
        public string? message { get; set; }
        public string? stack_trace { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }

    public class exceptionMiddleware
    {
        private readonly RequestDelegate _next;
        public exceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }
        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = context.Response.StatusCode == 200 ? (int)HttpStatusCode.InternalServerError : context.Response.StatusCode;
            await context.Response.WriteAsync(new ErrorDetails()
            {
                status_code = context.Response.StatusCode,
                message = exception.Message,
                stack_trace = exception.StackTrace
            }.ToString());
        }
    }
}
