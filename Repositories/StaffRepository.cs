using Microsoft.EntityFrameworkCore;
using StaffManagementApi.Data;
using StaffManagementApi.Interfaces;
using StaffManagementApi.Models;

namespace StaffManagementApi.Repositories;

public class StaffRepository : IStaffRepository
{
    private readonly ApplicationDbContext _db;
    public StaffRepository(ApplicationDbContext db) => _db = db;
    public async Task<IEnumerable<Staff>> GetAllAsync() => await _db.Staffs.ToListAsync();
    public async Task<Staff?> GetByIdAsync(int id) => await _db.Staffs.FindAsync(id);
    public async Task AddAsync(Staff staff) { await _db.Staffs.AddAsync(staff); await _db.SaveChangesAsync(); }
    public async Task UpdateAsync(Staff staff) { _db.Staffs.Update(staff); await _db.SaveChangesAsync(); }
    public async Task DeleteAsync(int id)
    {
        var s = await GetByIdAsync(id);
        if (s != null) { _db.Staffs.Remove(s); await _db.SaveChangesAsync(); }
    }
}