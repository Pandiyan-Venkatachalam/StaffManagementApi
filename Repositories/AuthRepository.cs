using Microsoft.EntityFrameworkCore;
using StaffManagementApi.Data;
using StaffManagementApi.Interfaces;
using StaffManagementApi.Models;

namespace StaffManagementApi.Repositories;

public class AuthRepository : IAuthRepository
{
    private readonly ApplicationDbContext _db;
    public AuthRepository(ApplicationDbContext db) => _db = db;

    public async Task<AppUser?> GetUserAsync(string username, string password)
    {
        return await _db.AppUsers
            .FirstOrDefaultAsync(u => u.Username == username && u.Password == password);
    }

    public async Task RegisterAsync(AppUser user)
    {
        user.IsActive = true;
        _db.AppUsers.Add(user);
        await _db.SaveChangesAsync();
    }

    public async Task<bool> UserExistsAsync(string username) =>
        await _db.AppUsers.AnyAsync(u => u.Username == username);

    public async Task<IEnumerable<object>> GetAccessListAsync()
    {
        return await (from s in _db.Staffs
                      join u in _db.AppUsers on s.AppUserId equals u.Id into userGroup
                      from u in userGroup.DefaultIfEmpty()
                      select new
                      {
                          Id = u != null ? u.Id : 0,
                          UserName = u != null ? u.Username : "No Account",
                          StaffName = s.Name,
                          Role = u != null ? u.Role : "N/A",
                          IsActive = u != null ? u.IsActive : false,
                          HasAccount = u != null
                      }).ToListAsync();
    }

    // New: Dropdown users list logic
    public async Task<IEnumerable<object>> GetUnlinkedUsersAsync()
    {
        return await _db.AppUsers
            .Where(u => !_db.Staffs.Any(s => s.AppUserId == u.Id))
            .Select(u => new { u.Id, u.Username })
            .ToListAsync();
    }

    public async Task LinkStaffToUserAsync(string username, int userId)
    {
        var staff = await _db.Staffs.FirstOrDefaultAsync(s => s.Name.ToLower() == username.ToLower());
        if (staff != null)
        {
            staff.AppUserId = userId;
            await _db.SaveChangesAsync();
        }
    }

    public async Task<bool> ToggleUserStatusAsync(int userId, bool isActive)
    {
        var user = await _db.AppUsers.FindAsync(userId);
        if (user == null) return false;
        user.IsActive = isActive;
        _db.AppUsers.Update(user);
        await _db.SaveChangesAsync();
        return true;
    }
}