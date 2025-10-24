using Microsoft.AspNetCore.Mvc;
using rappi.Application.DTOs;
using rappi.Application.Interfaces;
using rappi.Domain.Models;

namespace rappi.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomersController : ControllerBase
{
    private readonly ICustomerService _svc;

    public CustomersController(ICustomerService svc) => _svc = svc;

    [HttpGet]
    public async Task<IActionResult> Get() => Ok(await _svc.GetAllAsync());

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id) =>
        (await _svc.GetByIdAsync(id)) is Customer c ? Ok(c) : NotFound();

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CustomerCreateDto dto)
    {
        var customer = new Customer { Name = dto.Name, Email = dto.Email };
        var created = await _svc.AddAsync(customer);
        return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Put(int id, Customer c) =>
        await _svc.UpdateAsync(id, c) ? Ok(c) : NotFound();

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id) =>
        await _svc.DeleteAsync(id) ? NoContent() : NotFound();
}