using System.Text.Json.Serialization;

namespace StaffManagementApi.Models;
public class Staff {
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Department { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public decimal Salary { get; set; }
    public int? AppUserId { get; set; }

    [JsonIgnore]
    public virtual AppUser? AppUser { get; set; }
}