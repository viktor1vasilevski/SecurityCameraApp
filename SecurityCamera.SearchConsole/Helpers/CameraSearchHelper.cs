using Microsoft.Extensions.Hosting;
using SecurityCamera.Application.Requests.Camera;
using SecurityCamera.SearchConsole.Extensions;

namespace SecurityCamera.SearchConsole.Helpers;

public static class CameraSearchHelper
{
    public static async Task RunCameraSearchAsync(IHost host, string nameFilter)
    {
        await host.Services.UseCameraServiceAsync(async service =>
        {
            var response = await service.SearchCamerasByNameAsync(new CameraRequest { Name = nameFilter });

            if (response.Data is { Count: > 0 })
            {
                CameraPrintHelper.PrintCameras(response.Data);
            }
            else
            {
                MessagePrintHelper.PrintMessage("No cameras found matching your search.", ConsoleColor.Blue);
            }
        });
    }
}
