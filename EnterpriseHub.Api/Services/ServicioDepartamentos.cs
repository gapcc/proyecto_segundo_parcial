using EnterpriseHub.Api.Contracts;
using EnterpriseHub.Api.Data;
using EnterpriseHub.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace EnterpriseHub.Api.Services;

public class ServicioDepartamentos(ContextoEnterpriseHub context) : IServicioDepartamentos
{
    public async Task<IReadOnlyList<DepartamentoDto>> GetAllAsync() =>
        await context.Departments
            .OrderBy(item => item.Name)
            .Select(item => new DepartamentoDto(
                item.Id,
                item.Name,
                item.ManagerName,
                item.MonthlyBudget,
                item.Employees.Count))
            .ToListAsync();

    public async Task<DepartamentoDto?> GetByIdAsync(int id) =>
        await context.Departments
            .Where(item => item.Id == id)
            .Select(item => new DepartamentoDto(
                item.Id,
                item.Name,
                item.ManagerName,
                item.MonthlyBudget,
                item.Employees.Count))
            .FirstOrDefaultAsync();

    public async Task<DepartamentoDto> CreateAsync(GuardarDepartamentoRequest request)
    {
        var departamento = new Departamento
        {
            Name = request.Name.Trim(),
            ManagerName = request.ManagerName.Trim(),
            MonthlyBudget = request.MonthlyBudget
        };

        context.Departments.Add(departamento);
        await context.SaveChangesAsync();

        return new DepartamentoDto(departamento.Id, departamento.Name, departamento.ManagerName, departamento.MonthlyBudget, 0);
    }

    public async Task<DepartamentoDto?> UpdateAsync(int id, GuardarDepartamentoRequest request)
    {
        var departamento = await context.Departments.FindAsync(id);
        if (departamento is null)
        {
            return null;
        }

        departamento.Name = request.Name.Trim();
        departamento.ManagerName = request.ManagerName.Trim();
        departamento.MonthlyBudget = request.MonthlyBudget;

        await context.SaveChangesAsync();

        return await GetByIdAsync(id);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var departamento = await context.Departments
            .Include(item => item.Employees)
            .FirstOrDefaultAsync(item => item.Id == id);

        if (departamento is null || departamento.Employees.Count > 0)
        {
            return false;
        }

        context.Departments.Remove(departamento);
        await context.SaveChangesAsync();
        return true;
    }
}
