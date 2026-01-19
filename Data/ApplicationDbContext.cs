using Microsoft.EntityFrameworkCore;
using StaffManagementApi.Models;

namespace StaffManagementApi.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    public DbSet<Staff> Staffs { get; set; }
    public DbSet<UserTask> Tasks { get; set; }
    public DbSet<AppUser> AppUsers { get; set; }
}
