using EnterpriseHub.Api.Contracts;

namespace EnterpriseHub.Api.Services;

public interface IServicioDepartamentos
{
    Task<IReadOnlyList<DepartamentoDto>> GetAllAsync();
    Task<DepartamentoDto?> GetByIdAsync(int id);
    Task<DepartamentoDto> CreateAsync(GuardarDepartamentoRequest request);
    Task<DepartamentoDto?> UpdateAsync(int id, GuardarDepartamentoRequest request);
    Task<bool> DeleteAsync(int id);
}
