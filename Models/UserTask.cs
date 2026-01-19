namespace StaffManagementApi.Models;
public class UserTask {
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string AssignedTo { get; set; } = string.Empty;
    public string Deadline { get; set; } = string.Empty;
    public string Status { get; set; } = "Pending";
    public string AssignedAt { get; set; } = DateTime.UtcNow.ToString("dd/MM/yyyy");
}