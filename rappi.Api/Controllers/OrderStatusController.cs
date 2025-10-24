using Microsoft.AspNetCore.Mvc;
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

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] OrderStatus status)
    {
        try
        {
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