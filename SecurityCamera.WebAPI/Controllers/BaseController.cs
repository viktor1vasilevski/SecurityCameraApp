using Microsoft.AspNetCore.Mvc;
using SecurityCamera.Application.Enums;
using SecurityCamera.Application.Responses;

namespace SecurityCamera.WebAPI.Controllers
{
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
        protected IActionResult HandleResponse<TEntity>(ApiResponse<TEntity> response) where TEntity : class
        {
            return response.NotificationType switch
            {
                NotificationType.Success => Ok(response),
                NotificationType.ServerError => StatusCode(500, response),
                _ => Ok(response),
            };
        }
    }
}
