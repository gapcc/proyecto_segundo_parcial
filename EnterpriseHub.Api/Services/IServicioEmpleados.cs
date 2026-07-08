using EnterpriseHub.Api.Contracts;

namespace EnterpriseHub.Api.Services;

public interface IServicioEmpleados
{
    Task<IReadOnlyList<EmpleadoDto>> GetAllAsync();
    Task<EmpleadoDto?> GetByIdAsync(int id);
    Task<EmpleadoDto> CreateAsync(GuardarEmpleadoRequest request);
    Task<EmpleadoDto?> UpdateAsync(int id, GuardarEmpleadoRequest request);
    Task<bool> DeleteAsync(int id);
}
