namespace StaffManagementApi.Interfaces;
using StaffManagementApi.Models;

public interface IAuthService {
    Task<string?> LoginAsync(string username, string password);
    Task<bool> RegisterAsync(AppUser user);
}