using SecurityCamera.Application.DTOs.Camera;
using SecurityCamera.Application.Enums;
using SecurityCamera.Application.Interfaces.Repositories;
using SecurityCamera.Application.Interfaces.Services;
using SecurityCamera.Application.Requests.Camera;
using SecurityCamera.Application.Responses;
using Microsoft.Extensions.Logging;
using SecurityCamera.Application.Constants;
using SecurityCamera.Application.Extensions;
using SecurityCamera.Application.Exceptions.Csv;

namespace SecurityCamera.Application.Services;

public class CameraService(ICameraRepository cameraRepository, ILogger<CameraService> logger) : ICameraService
{
    public async Task<ApiResponse<CameraGroupedDTO>> GetGroupedCamerasAsync()
    {
        try
        {
            var cameras = await cameraRepository.LoadCsvAsync();

            var grouped = new CameraGroupedDTO();

            foreach (var cam in cameras)
            {
                var dto = new CameraDTO
                {
                    Number = cam.Number,
                    Name = cam.Name,
                    Latitude = cam.Latitude,
                    Longitude = cam.Longitude
                };

                if (cam.Number % 3 == 0 && cam.Number % 5 == 0)
                    grouped.DivisibleBy3And5.Add(dto);
                else if (cam.Number % 3 == 0)
                    grouped.DivisibleBy3.Add(dto);
                else if (cam.Number % 5 == 0)
                    grouped.DivisibleBy5.Add(dto);
                else
                    grouped.NotDivisible.Add(dto);
            }

            return new ApiResponse<CameraGroupedDTO>
            {
                NotificationType = NotificationType.Success,
                Data = grouped,
            };
        }
        catch (CsvParseException ex)
        {
            logger.LogError(ex, "Exception ocured in [{Function}] at [{Timestamp}]",
                nameof(GetGroupedCamerasAsync), DateTime.Now);

            return new ApiResponse<CameraGroupedDTO>
            {
                Message = CameraConstants.DataFormatInvalid,
                NotificationType = NotificationType.ServerError
            };
        }
        catch (DataLoadException ex)
        {
            logger.LogError(ex, "Exception ocured in [{Function}] at [{Timestamp}]",
                nameof(GetGroupedCamerasAsync), DateTime.Now);

            return new ApiResponse<CameraGroupedDTO>
            {
                Message = CameraConstants.FailedLoadingCameraData,
                NotificationType = NotificationType.ServerError
            };
        }
    }

    public async Task<ApiResponse<List<CameraDTO>>> SearchCamerasByNameAsync(CameraRequest request)
    {
        try
        {
            var cameras = await cameraRepository.LoadCsvAsync();

            cameras = cameras.WhereIf(!string.IsNullOrEmpty(request.Name),
                x => x.Name.Contains(request.Name!, StringComparison.OrdinalIgnoreCase));

            var camerasDTOs = cameras.Select(x => new CameraDTO
            {
                Number = x.Number,
                Name = x.Name,
                Latitude = x.Latitude,
                Longitude = x.Longitude
            }).ToList();

            return new ApiResponse<List<CameraDTO>>
            {
                NotificationType = NotificationType.Success,
                Data = camerasDTOs,
            };
        }
        catch (CsvParseException ex)
        {
            logger.LogError(ex, "Exception ocured in [{Function}] at [{Timestamp}]",
                nameof(SearchCamerasByNameAsync), DateTime.Now);

            return new ApiResponse<List<CameraDTO>>
            {
                Message = CameraConstants.DataFormatInvalid,
                NotificationType = NotificationType.ServerError
            };
        }
        catch (DataLoadException ex)
        {
            logger.LogError(ex, "Exception ocured in [{Function}] at [{Timestamp}]",
                nameof(SearchCamerasByNameAsync), DateTime.Now);

            return new ApiResponse<List<CameraDTO>>
            {
                Message = CameraConstants.FailedLoadingCameraData,
                NotificationType = NotificationType.ServerError
            };
        }
    }
}
