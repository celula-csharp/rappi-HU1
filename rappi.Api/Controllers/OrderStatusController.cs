using Microsoft.AspNetCore.Mvc;
using rappi.Application.DTOs;
using rappi.Application.Services;
using rappi.Domain.Models;

namespace rappi.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderStatusController : ControllerBase
{
    private readonly OrderStatusService _svc;

    public OrderStatusController(OrderStatusService svc) => _svc = svc;

    [HttpGet]
    public async Task<IActionResult> Get() =>
        Ok(await _svc.GetAllAsync());

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id) =>
        (await _svc.GetByIdAsync(id)) is OrderStatus s ? Ok(s) : NotFound();

    // Usamos el nuevo DTO para mantener consistencia
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] OrderStatusDto dto)
    {
        try
        {
            var status = new OrderStatus { Name = dto.Name };
            var created = await _svc.AddAsync(status);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    // Nuevo método opcional para actualizar el nombre del estado
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Put(int id, [FromBody] OrderStatusDto dto)
    {
        try
        {
            var updated = await _svc.UpdateAsync(id, dto.Name);
            return Ok(updated);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            return await _svc.DeleteAsync(id)
                ? Ok()
                : NotFound();
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}
