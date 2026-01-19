using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using StaffManagementApi.Interfaces;
using StaffManagementApi.Models;

namespace StaffManagementApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class TaskController : ControllerBase
{
    private readonly ITaskService _service;
    public TaskController(ITaskService s) => _service = s;


    [HttpGet("{username}/{role}")]
    public async Task<IActionResult> Get(string username, string role)
    {
        Console.WriteLine($"Username: {username}, Role: {role}");

        var tasks = await _service.GetTasksAsync(username, role);
        return Ok(tasks);
    }

    [HttpPost]
    [Authorize(Roles = "Admin,admin")]
    public async Task<IActionResult> Post([FromBody] UserTask task)
    {

        if (string.IsNullOrEmpty(task.Status)) task.Status = "Pending";
        await _service.AddTaskAsync(task);
        return Ok(new { message = "Task assigned successfully" });
    }

    [HttpPatch("{id}/status")]
    public async Task<IActionResult> UpdateStatus(int id, [FromBody] string status)
    {
        await _service.UpdateTaskStatusAsync(id, status);
        return Ok(new { message = "Status updated" });
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin,admin")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteTaskAsync(id);
        return Ok(new { message = "Task deleted successfully" });
    }
}