using EnterpriseHub.Api.Contracts;
using EnterpriseHub.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace EnterpriseHub.Api.Controllers;

[ApiController]
[Route("api/employees")]
public class EmpleadosController(IServicioEmpleados service) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<EmpleadoDto>>> GetAll() =>
        Ok(await service.GetAllAsync());

    [HttpGet("{id:int}")]
    public async Task<ActionResult<EmpleadoDto>> GetById(int id)
    {
        var item = await service.GetByIdAsync(id);
        return item is null ? NotFound() : Ok(item);
    }

    [HttpPost]
    public async Task<ActionResult<EmpleadoDto>> Create(GuardarEmpleadoRequest request)
    {
        try
        {
            var item = await service.CreateAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = item.Id }, item);
        }
        catch (InvalidOperationException exception)
        {
            return BadRequest(exception.Message);
        }
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<EmpleadoDto>> Update(int id, GuardarEmpleadoRequest request)
    {
        try
        {
            var item = await service.UpdateAsync(id, request);
            return item is null ? NotFound() : Ok(item);
        }
        catch (InvalidOperationException exception)
        {
            return BadRequest(exception.Message);
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id) =>
        await service.DeleteAsync(id) ? NoContent() : NotFound();
}
