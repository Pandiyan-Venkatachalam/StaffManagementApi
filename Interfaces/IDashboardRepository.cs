namespace StaffManagementApi.Interfaces;

using StaffManagementApi.Models;

public interface IDashboardRepository {
    Task<DashboardSummary> GetDashboardStatsAsync();
}