using Microsoft.EntityFrameworkCore;
using StaffManagementApi.Data;
using StaffManagementApi.Interfaces;
using StaffManagementApi.Models;

namespace StaffManagementApi.Repositories;

public class TaskRepository : ITaskRepository
{
    private readonly ApplicationDbContext _db;
    public TaskRepository(ApplicationDbContext db) => _db = db;

    public async Task<IEnumerable<UserTask>> GetAllAsync() =>
        await _db.Tasks.OrderByDescending(t => t.Id).ToListAsync();

    public async Task<IEnumerable<UserTask>> GetByUsernameAsync(string username)
    {
        return await _db.Tasks
            .Where(t => t.AssignedTo.ToLower().Trim() == username.ToLower().Trim())
            .OrderByDescending(t => t.Id)
            .ToListAsync();
    }

    public async Task AddAsync(UserTask task)
    {
        _db.Tasks.Add(task);
        await _db.SaveChangesAsync();
    }

    public async Task UpdateStatusAsync(int id, string status)
    {
        var task = await _db.Tasks.FindAsync(id);
        if (task != null)
        {
            task.Status = status;
            await _db.SaveChangesAsync();
        }
    }

    public async Task DeleteAsync(int id)
    {
        var task = await _db.Tasks.FindAsync(id);
        if (task != null)
        {
            _db.Tasks.Remove(task);
            await _db.SaveChangesAsync();
        }
    }
}