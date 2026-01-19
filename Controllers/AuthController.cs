using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StaffManagementApi.Interfaces;
using StaffManagementApi.Models;

namespace StaffManagementApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _service;
    private readonly IAuthRepository _repo;

    public AuthController(IAuthService s, IAuthRepository r)
    {
        _service = s;
        _repo = r;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest loginData)
    {
        var user = await _repo.GetUserAsync(loginData.Username, loginData.Password);
        if (user == null || !user.IsActive)
            return Unauthorized(new { message = "Invalid credentials or account disabled" });

        var token = await _service.LoginAsync(loginData.Username, loginData.Password);
        if (token == null) return Unauthorized(new { message = "Token generation failed" });

        return Ok(new { token, role = user.Role, username = user.Username });
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] AppUser user)
    {
        var result = await _service.RegisterAsync(user);
        if (!result) return BadRequest(new { message = "Username already exists" });
        return Ok(new { message = "Registered successfully" });
    }

    [Authorize(Roles = "Admin,admin")]
    [HttpGet("access-list")]
    public async Task<IActionResult> GetAccessList() => Ok(await _repo.GetAccessListAsync());

    [Authorize(Roles = "Admin,admin")]
    [HttpGet("users-list")]
    public async Task<IActionResult> GetUnlinkedUsers() => Ok(await _repo.GetUnlinkedUsersAsync());

    [Authorize(Roles = "Admin,admin")]
    [HttpPatch("toggle-status/{id}")]
    public async Task<IActionResult> ToggleStatus(int id, [FromBody] bool isActive)
    {
        var success = await _repo.ToggleUserStatusAsync(id, isActive);
        if (!success) return NotFound(new { message = "User not found" });
        return Ok(new { message = "Status updated successfully" });
    }
}

public class LoginRequest
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}