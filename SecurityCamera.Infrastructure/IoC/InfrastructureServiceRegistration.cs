using Microsoft.Extensions.DependencyInjection;
using SecurityCamera.Application.Interfaces.Repositories;
using SecurityCamera.Application.Interfaces.Services;
using SecurityCamera.Application.Services;
using SecurityCamera.Infrastructure.Repositories;

namespace SecurityCamera.Infrastructure.IoC;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, string csvPath)
    {
        var fullPath = Path.GetFullPath(csvPath);
        services.AddSingleton<ICameraRepository>(new CsvCameraRepository(fullPath));

        services.AddScoped<ICameraService, CameraService>();

        return services;
    }
}
