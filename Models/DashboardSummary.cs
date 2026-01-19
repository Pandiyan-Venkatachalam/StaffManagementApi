namespace StaffManagementApi.Models;

public class DashboardSummary
{
    public int TotalStaff { get; set; }
    public int PendingTasks { get; set; }
    public int CompletedTasks { get; set; }
    public int ActiveUsers { get; set; }
    public decimal TotalPayroll { get; set; }
    public int ActiveDepartments { get; set; }
}
