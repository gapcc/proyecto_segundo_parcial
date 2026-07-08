using EnterpriseHub.Api.Contracts;
using EnterpriseHub.Api.Data;
using EnterpriseHub.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace EnterpriseHub.Api.Services;

public class ServicioSolicitudesPermiso(ContextoEnterpriseHub context) : IServicioSolicitudesPermiso
{
    public async Task<IReadOnlyList<SolicitudPermisoDto>> GetAllAsync() =>
        await context.LeaveRequests
            .Include(item => item.Empleado)
            .OrderByDescending(item => item.StartDate)
            .ToListAsync()
            .ContinueWith(task => (IReadOnlyList<SolicitudPermisoDto>)task.Result.Select(ToDto).ToList());

    public async Task<SolicitudPermisoDto?> GetByIdAsync(int id)
    {
        var solicitudPermiso = await context.LeaveRequests
            .Include(item => item.Empleado)
            .FirstOrDefaultAsync(item => item.Id == id);

        return solicitudPermiso is null ? null : ToDto(solicitudPermiso);
    }

    public async Task<SolicitudPermisoDto> CreateAsync(GuardarSolicitudPermisoRequest request)
    {
        await ValidateEmployeeAsync(request.EmployeeId);
        ValidateDates(request.StartDate, request.EndDate);

        var solicitudPermiso = new SolicitudPermiso
        {
            EmployeeId = request.EmployeeId,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            Reason = request.Reason.Trim(),
            Status = request.Status.Trim()
        };

        context.LeaveRequests.Add(solicitudPermiso);
        await context.SaveChangesAsync();

        return (await GetByIdAsync(solicitudPermiso.Id))!;
    }

    public async Task<SolicitudPermisoDto?> UpdateAsync(int id, GuardarSolicitudPermisoRequest request)
    {
        var solicitudPermiso = await context.LeaveRequests.FindAsync(id);
        if (solicitudPermiso is null)
        {
            return null;
        }

        await ValidateEmployeeAsync(request.EmployeeId);
        ValidateDates(request.StartDate, request.EndDate);

        solicitudPermiso.EmployeeId = request.EmployeeId;
        solicitudPermiso.StartDate = request.StartDate;
        solicitudPermiso.EndDate = request.EndDate;
        solicitudPermiso.Reason = request.Reason.Trim();
        solicitudPermiso.Status = request.Status.Trim();

        await context.SaveChangesAsync();

        return await GetByIdAsync(id);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var solicitudPermiso = await context.LeaveRequests.FindAsync(id);
        if (solicitudPermiso is null)
        {
            return false;
        }

        context.LeaveRequests.Remove(solicitudPermiso);
        await context.SaveChangesAsync();
        return true;
    }

    private async Task ValidateEmployeeAsync(int employeeId)
    {
        var exists = await context.Employees.AnyAsync(item => item.Id == employeeId);
        if (!exists)
        {
            throw new InvalidOperationException("El empleado seleccionado no existe.");
        }
    }

    private static void ValidateDates(DateTime startDate, DateTime endDate)
    {
        if (endDate < startDate)
        {
            throw new InvalidOperationException("La fecha final no puede ser menor que la inicial.");
        }
    }

    private static SolicitudPermisoDto ToDto(SolicitudPermiso item)
    {
        var totalDays = Math.Max(1, (item.EndDate.Date - item.StartDate.Date).Days + 1);
        var employeeName = item.Empleado is null ? string.Empty : $"{item.Empleado.FirstName} {item.Empleado.LastName}";

        return new SolicitudPermisoDto(
            item.Id,
            item.EmployeeId,
            employeeName,
            item.StartDate,
            item.EndDate,
            item.Reason,
            item.Status,
            totalDays);
    }
}
