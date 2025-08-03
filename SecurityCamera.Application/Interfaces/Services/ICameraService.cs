using SecurityCamera.Application.DTOs.Camera;
using SecurityCamera.Application.Requests.Camera;
using SecurityCamera.Application.Responses;

namespace SecurityCamera.Application.Interfaces.Services;

public interface ICameraService
{
    Task<ApiResponse<CameraGroupedDTO>> GetGroupedCamerasAsync();
    Task<ApiResponse<List<CameraDTO>>> SearchCamerasByNameAsync(CameraRequest request);

}