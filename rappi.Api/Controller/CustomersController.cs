using Microsoft.AspNetCore.Mvc;
using rappi.Application.Services;
using rappi.Domain.Entities;

namespace rappi.Api.Controller;

[ApiController]
[Route("api/[controller]")]
public class CustomersController :ControllerBase
{
    private readonly CustomerService _service;
    public CustomersController(CustomerService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var list = await _service.GetAllAsync();
        return Ok(list);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var c = await _service.GetByIdAsync(id);
        if (c == null) return NotFound();
        return Ok(c);
    }

    [HttpPost]
    public async Task<IActionResult>([FromBody] Customer _customer)
    {
        if(string.IsNullOrWhiteSpace(_customer.Name)) || string.IsNullOrWhiteSpace(_customer.Email)
        return BadRequest("Name and Email are required.");

        var created = await _service.CreateAsync(_customer);
        return CreatedAtAction((nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("id:int")]
    public async Task<IActionResult> Update(int id, [FromBody] Customer customer)
    {
        if (id != customer.Id) return BadRequest("Id mismatch");
        var ok = await _service.UpdateAsync(customer);
        if (!ok) return NotFound();
        return NoContent();
    }

    [HttpDelete("id:int")]
    public async Task<IActionResult> Delete(int id)
    {
        var ok = await _service.DeleteAsync(id);
        if (!ok) return NotFound();
        return NotFound();
    }
}