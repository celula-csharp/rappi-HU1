using Microsoft.AspNetCore.Mvc;
using rappi.Application.DTOs;
using rappi.Application.Services;
using rappi.Domain.Models;

namespace rappi.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    private readonly OrderService _svc;
    public OrderController(OrderService svc) => _svc = svc;
    
    [HttpGet]
    public async Task<IActionResult> Get() => 
        Ok(await _svc.GetAllAsync());

    [HttpGet("{Id:int")]
    public async Task<IActionResult> GetById(int id) =>
        (await _svc.GetByIdAsync(id))
        is Order o 
            ? Ok(o) 
            : NotFound();

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] OrderCreateDto dto)
    {
        var Order = new Order
        {
            CustomerId = dto.CustomerId,
            OrderDate = dto.OrderDate
        };
        var create = await _svc.AddAsync(Order);
        return CreatedAtAction(nameof(GetById), new { id = create.Id }, create);
    }
}