using StaffManagementApi.Interfaces;
using StaffManagementApi.Models;
// using RestSharp;

namespace StaffManagementApi.Services;

public class DashboardService : IDashboardService {
    private readonly IDashboardRepository _repo;
    private readonly ILogger<DashboardService> _logger;

    public DashboardService(IDashboardRepository repo, ILogger<DashboardService> logger) {
        _repo = repo;
        _logger = logger;
    }

    public async Task<DashboardSummary> GetDashboardSummaryAsync() {
        try {
            _logger.LogInformation("Dashboard stats requested.");

            // Example: Using RestSharp to notify an external audit service
            // var client = new RestClient("https://audit-api.com");
            // var request = new RestRequest("/log-access", Method.Post);
            // request.AddJsonBody(new { Action = "DashboardView", Time = DateTime.UtcNow });
            // await client.ExecuteAsync(request);

            return await _repo.GetDashboardStatsAsync();
        } catch (Exception ex) {
            _logger.LogError(ex, "Error fetching dashboard stats from PostgreSQL");
            throw; 
        }
    }
}