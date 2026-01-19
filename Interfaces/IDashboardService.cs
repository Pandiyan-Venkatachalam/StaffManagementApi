using StaffManagementApi.Models;

namespace StaffManagementApi.Interfaces;

public interface IDashboardService {
    Task<DashboardSummary> GetDashboardSummaryAsync();
}