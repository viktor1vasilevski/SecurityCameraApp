using SecurityCamera.Domain.Entities;

namespace SecurityCamera.Application.Interfaces.Repositories;

public interface ICameraRepository
{
    Task<IEnumerable<Camera>> LoadCsvAsync();
}
