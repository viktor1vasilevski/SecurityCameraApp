using Microsoft.Extensions.DependencyInjection;
using SecurityCamera.Application.Interfaces.Services;

namespace SecurityCamera.SearchConsole.Extensions;

public static class ServiceProviderExtensions
{
    public static async Task UseCameraServiceAsync(this IServiceProvider services, Func<ICameraService, Task> action)
    {
        using var scope = services.CreateScope();
        var service = scope.ServiceProvider.GetRequiredService<ICameraService>();
        await action(service);
    }
}
