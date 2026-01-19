namespace StaffManagementApi.Models;
public class AppUser {
    public int Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Role { get; set; } = "Staff"; // Admin or Staff
    public bool IsActive { get; set; } = true; // The Privilege Toggle
}