using EnterpriseHub.Api.Contracts;
using EnterpriseHub.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace EnterpriseHub.Api.Controllers;

[ApiController]
[Route("api/departments")]
public class DepartamentosController(IServicioDepartamentos service) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<DepartamentoDto>>> GetAll() =>
        Ok(await service.GetAllAsync());

    [HttpGet("{id:int}")]
    public async Task<ActionResult<DepartamentoDto>> GetById(int id)
    {
        var item = await service.GetByIdAsync(id);
        return item is null ? NotFound() : Ok(item);
    }

    [HttpPost]
    public async Task<ActionResult<DepartamentoDto>> Create(GuardarDepartamentoRequest request)
    {
        var item = await service.CreateAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = item.Id }, item);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<DepartamentoDto>> Update(int id, GuardarDepartamentoRequest request)
    {
        var item = await service.UpdateAsync(id, request);
        return item is null ? NotFound() : Ok(item);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id) =>
        await service.DeleteAsync(id) ? NoContent() : BadRequest("No se puede eliminar el departamento porque tiene empleados asociados o no existe.");
}
