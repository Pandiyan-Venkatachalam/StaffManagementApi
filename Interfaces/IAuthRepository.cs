namespace StaffManagementApi.Interfaces;
using StaffManagementApi.Models;

public interface IAuthRepository {
    Task<AppUser?> GetUserAsync(string username, string password);
    Task RegisterAsync(AppUser user);
    Task<bool> UserExistsAsync(string username);
    Task<bool> ToggleUserStatusAsync(int userId, bool isActive);
    Task<IEnumerable<object>> GetUnlinkedUsersAsync(); 
    Task<IEnumerable<object>> GetAccessListAsync();

    Task LinkStaffToUserAsync(string username, int userId);
}