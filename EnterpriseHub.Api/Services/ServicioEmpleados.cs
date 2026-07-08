using EnterpriseHub.Api.Contracts;
using EnterpriseHub.Api.Data;
using EnterpriseHub.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace EnterpriseHub.Api.Services;

public class ServicioEmpleados(ContextoEnterpriseHub context) : IServicioEmpleados
{
    public async Task<IReadOnlyList<EmpleadoDto>> GetAllAsync() =>
        await context.Employees
            .Include(item => item.Departamento)
            .Include(item => item.Cargo)
            .OrderBy(item => item.LastName)
            .ThenBy(item => item.FirstName)
            .ToListAsync()
            .ContinueWith(task => (IReadOnlyList<EmpleadoDto>)task.Result.Select(ToDto).ToList());

    public async Task<EmpleadoDto?> GetByIdAsync(int id)
    {
        var empleado = await context.Employees
            .Include(item => item.Departamento)
            .Include(item => item.Cargo)
            .FirstOrDefaultAsync(item => item.Id == id);

        return empleado is null ? null : ToDto(empleado);
    }

    public async Task<EmpleadoDto> CreateAsync(GuardarEmpleadoRequest request)
    {
        await ValidateRelationsAsync(request.DepartmentId, request.JobPositionId);

        var empleado = new Empleado
        {
            FirstName = request.FirstName.Trim(),
            LastName = request.LastName.Trim(),
            Email = request.Email.Trim(),
            HireDate = request.HireDate,
            Status = request.Status.Trim(),
            DepartmentId = request.DepartmentId,
            JobPositionId = request.JobPositionId
        };

        context.Employees.Add(empleado);
        await context.SaveChangesAsync();

        return (await GetByIdAsync(empleado.Id))!;
    }

    public async Task<EmpleadoDto?> UpdateAsync(int id, GuardarEmpleadoRequest request)
    {
        var empleado = await context.Employees.FindAsync(id);
        if (empleado is null)
        {
            return null;
        }

        await ValidateRelationsAsync(request.DepartmentId, request.JobPositionId);

        empleado.FirstName = request.FirstName.Trim();
        empleado.LastName = request.LastName.Trim();
        empleado.Email = request.Email.Trim();
        empleado.HireDate = request.HireDate;
        empleado.Status = request.Status.Trim();
        empleado.DepartmentId = request.DepartmentId;
        empleado.JobPositionId = request.JobPositionId;

        await context.SaveChangesAsync();

        return await GetByIdAsync(id);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var empleado = await context.Employees
            .Include(item => item.LeaveRequests)
            .FirstOrDefaultAsync(item => item.Id == id);

        if (empleado is null)
        {
            return false;
        }

        context.Employees.Remove(empleado);
        await context.SaveChangesAsync();
        return true;
    }

    private async Task ValidateRelationsAsync(int departmentId, int jobPositionId)
    {
        var departmentExists = await context.Departments.AnyAsync(item => item.Id == departmentId);
        var jobPositionExists = await context.JobPositions.AnyAsync(item => item.Id == jobPositionId);

        if (!departmentExists || !jobPositionExists)
        {
            throw new InvalidOperationException("La relacion del empleado no es valida.");
        }
    }

    private static EmpleadoDto ToDto(Empleado item) =>
        new(
            item.Id,
            item.FirstName,
            item.LastName,
            item.Email,
            item.HireDate,
            item.Status,
            item.DepartmentId,
            item.Departamento?.Name ?? string.Empty,
            item.JobPositionId,
            item.Cargo?.Title ?? string.Empty);
}
