using EnterpriseHub.Api.Contracts;

namespace EnterpriseHub.Api.Services;

public interface IServicioCargos
{
    Task<IReadOnlyList<CargoDto>> GetAllAsync();
    Task<CargoDto?> GetByIdAsync(int id);
    Task<CargoDto> CreateAsync(GuardarCargoRequest request);
    Task<CargoDto?> UpdateAsync(int id, GuardarCargoRequest request);
    Task<bool> DeleteAsync(int id);
}
