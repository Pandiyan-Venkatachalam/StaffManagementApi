using StaffManagementApi.Interfaces; 
using StaffManagementApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace StaffManagementApi.Services;

public class AuthService : IAuthService {
    private readonly IAuthRepository _repo;
    private readonly IConfiguration _config;

    public AuthService(IAuthRepository repo, IConfiguration config) {
        _repo = repo;
        _config = config;
    }

    public async Task<string?> LoginAsync(string username, string password) {
        var user = await _repo.GetUserAsync(username, password);
        if (user == null || !user.IsActive) return null; 

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_config["Jwt:Key"]!);
        var tokenDescriptor = new SecurityTokenDescriptor {
            Subject = new ClaimsIdentity(new[] {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role) 
            }),
            Expires = DateTime.UtcNow.AddHours(5),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            Issuer = _config["Jwt:Issuer"],
            Audience = _config["Jwt:Audience"]
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public async Task<bool> RegisterAsync(AppUser user) {
        if (await _repo.UserExistsAsync(user.Username)) return false;
        await _repo.RegisterAsync(user);
        
        // Auto-Link logic
        await _repo.LinkStaffToUserAsync(user.Username, user.Id);
        return true;
    }
}