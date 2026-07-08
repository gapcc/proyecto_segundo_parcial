using EnterpriseHub.Api.Contracts;

namespace EnterpriseHub.Api.Services;

public interface IServicioSolicitudesPermiso
{
    Task<IReadOnlyList<SolicitudPermisoDto>> GetAllAsync();
    Task<SolicitudPermisoDto?> GetByIdAsync(int id);
    Task<SolicitudPermisoDto> CreateAsync(GuardarSolicitudPermisoRequest request);
    Task<SolicitudPermisoDto?> UpdateAsync(int id, GuardarSolicitudPermisoRequest request);
    Task<bool> DeleteAsync(int id);
}
