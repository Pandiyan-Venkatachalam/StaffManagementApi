using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StaffManagementApi.Interfaces;

namespace StaffManagementApi.Controllers;

[Authorize(Roles = "admin")] 
[ApiController] 
[Route("api/[controller]")]
public class DashboardController : ControllerBase {
    private readonly IDashboardService _service;

    public DashboardController(IDashboardService s) => _service = s;

    [HttpGet("summary")]
    public async Task<IActionResult> GetSummary() {
        var stats = await _service.GetDashboardSummaryAsync();
        return Ok(stats);
    }
}