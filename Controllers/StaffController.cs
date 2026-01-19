using Microsoft.AspNetCore.Mvc;
using StaffManagementApi.Interfaces;
using StaffManagementApi.Models;

namespace StaffManagementApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StaffController : ControllerBase
{
    private readonly IStaffService _service;
    public StaffController(IStaffService s) => _service = s;

    [HttpGet] 
    public async Task<IActionResult> Get() => Ok(await _service.GetStaffsAsync());

    [HttpPost] 
    public async Task<IActionResult> Post([FromBody] Staff s) 
    { 
        await _service.AddStaffAsync(s); 
        return Ok(); 
    }

    [HttpPut("{id}")] 
    public async Task<IActionResult> Put(int id, [FromBody] Staff s) 
    { 
        await _service.UpdateStaffAsync(s); 
        return Ok(); 
    }

    [HttpDelete("{id}")] 
    public async Task<IActionResult> Delete(int id) 
    { 
        await _service.DeleteStaffAsync(id); 
        return Ok(); 
    }
}