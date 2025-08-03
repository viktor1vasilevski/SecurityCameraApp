using SecurityCamera.Application.Enums;
using SecurityCamera.Application.Responses;
using System.Net;
using System.Text.Json;

namespace SecurityCamera.WebAPI.Middlewares
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;

        public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception caught in global middleware.");
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";

                var response = new ApiResponse<object>
                {
                    Message = "An unexpected error occurred. Please try again later.",
                    NotificationType = NotificationType.ServerError,
                    Data = null
                };

                var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
                var jsonResponse = JsonSerializer.Serialize(response, options);

                await context.Response.WriteAsync(jsonResponse);
            }
        }
    }
}
