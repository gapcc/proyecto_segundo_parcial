using EnterpriseHub.Api.Contracts;
using EnterpriseHub.Api.Data;
using EnterpriseHub.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace EnterpriseHub.Api.Services;

public class ServicioCargos(ContextoEnterpriseHub context) : IServicioCargos
{
    public async Task<IReadOnlyList<CargoDto>> GetAllAsync() =>
        await context.JobPositions
            .OrderBy(item => item.Title)
            .Select(item => new CargoDto(
                item.Id,
                item.Title,
                item.Level,
                item.BaseSalary,
                item.Employees.Count))
            .ToListAsync();

    public async Task<CargoDto?> GetByIdAsync(int id) =>
        await context.JobPositions
            .Where(item => item.Id == id)
            .Select(item => new CargoDto(
                item.Id,
                item.Title,
                item.Level,
                item.BaseSalary,
                item.Employees.Count))
            .FirstOrDefaultAsync();

    public async Task<CargoDto> CreateAsync(GuardarCargoRequest request)
    {
        var cargo = new Cargo
        {
            Title = request.Title.Trim(),
            Level = request.Level.Trim(),
            BaseSalary = request.BaseSalary
        };

        context.JobPositions.Add(cargo);
        await context.SaveChangesAsync();

        return new CargoDto(cargo.Id, cargo.Title, cargo.Level, cargo.BaseSalary, 0);
    }

    public async Task<CargoDto?> UpdateAsync(int id, GuardarCargoRequest request)
    {
        var cargo = await context.JobPositions.FindAsync(id);
        if (cargo is null)
        {
            return null;
        }

        cargo.Title = request.Title.Trim();
        cargo.Level = request.Level.Trim();
        cargo.BaseSalary = request.BaseSalary;
        await context.SaveChangesAsync();

        return await GetByIdAsync(id);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var cargo = await context.JobPositions
            .Include(item => item.Employees)
            .FirstOrDefaultAsync(item => item.Id == id);

        if (cargo is null || cargo.Employees.Count > 0)
        {
            return false;
        }

        context.JobPositions.Remove(cargo);
        await context.SaveChangesAsync();
        return true;
    }
}
