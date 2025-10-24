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

    // GET: api/orderstatus
    [HttpGet]
    public async Task<IActionResult> Get() => Ok(await _svc.GetAllAsync());

    // GET: api/orderstatus/5
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id) =>
        (await _svc.GetByIdAsync(id)) is OrderStatus s ? Ok(s) : NotFound();

    // POST: api/orderstatus
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] OrderStatus status)
    {
        var created = await _svc.AddAsync(status);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    // DELETE: api/orderstatus/5
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id) =>
        await _svc.DeleteAsync(id) ? Ok() : NotFound();
}