using Dapper;
using Npgsql;
using StaffManagementApi.Interfaces;
using StaffManagementApi.Models;
using Microsoft.Extensions.Configuration;

namespace StaffManagementApi.Repositories;

public class DashboardRepository : IDashboardRepository
{
    private readonly IConfiguration _config;

    public DashboardRepository(IConfiguration config)
    {
        _config = config;
    }

    public async Task<DashboardSummary> GetDashboardStatsAsync()
    {
        using var connection = new NpgsqlConnection(
            _config.GetConnectionString("DefaultConnection"));

        var result = await connection.QueryFirstOrDefaultAsync<DashboardSummary>(
            "SELECT * FROM student_schema.get_dashboard_summary()"
        );

        return result ?? new DashboardSummary();
    }
}
