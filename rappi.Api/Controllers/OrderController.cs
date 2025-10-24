using Microsoft.AspNetCore.Mvc;
using rappi.Application.DTOs;
using rappi.Application.Interfaces;
using rappi.Domain.Models;

namespace rappi.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    private readonly IOrderService _svc;

    public OrderController(IOrderService svc) => _svc = svc;

    [HttpGet]
    public async Task<IActionResult> Get() => Ok(await _svc.GetAllAsync());

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id) =>
        (await _svc.GetByIdAsync(id)) is Order o ? Ok(o) : NotFound();

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] OrderCreateDto dto)
    {
        var order = new Order
        {
            CustomerId = dto.CustomerId,
            StatusId = dto.StatusId,
            OrderDate = dto.OrderDate
        };
        var created = await _svc.AddAsync(order);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Put(int id, [FromBody] OrderUpdateDto dto) =>
        await _svc.UpdateStatusAsync(id, dto.StatusId)
            ? Ok(new { Id = id, StatusId = dto.StatusId })
            : BadRequest("Estado inválido");

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id) =>
        await _svc.DeleteAsync(id) ? Ok() : NotFound();
}