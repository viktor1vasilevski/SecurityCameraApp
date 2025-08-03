using Microsoft.AspNetCore.Mvc;
using SecurityCamera.Application.Interfaces.Services;

namespace SecurityCamera.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CameraController(ICameraService cameraService) : BaseController
    {


        [HttpGet("grouped")]
        public async Task<IActionResult> GetGroupedAsync()
        {
            var response = await cameraService.GetGroupedCamerasAsync();
            return HandleResponse(response);
        }
    }
}
