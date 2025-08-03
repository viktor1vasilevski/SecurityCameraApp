using SecurityCamera.Application.Enums;

namespace SecurityCamera.Application.Responses;

public class ApiResponse<T> where T : class
{
    public T? Data { get; set; }
    public string? Message { get; set; }
    public NotificationType NotificationType { get; set; }
}
