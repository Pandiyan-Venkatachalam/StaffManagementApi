using StaffManagementApi.Interfaces;
using StaffManagementApi.Models;

namespace StaffManagementApi.Services;
public class StaffService : IStaffService {
    private readonly IStaffRepository _repo;
    public StaffService(IStaffRepository repo) => _repo = repo;
    public async Task<IEnumerable<Staff>> GetStaffsAsync() => await _repo.GetAllAsync();
    public async Task AddStaffAsync(Staff staff) => await _repo.AddAsync(staff);
    public async Task UpdateStaffAsync(Staff staff) => await _repo.UpdateAsync(staff);
    public async Task DeleteStaffAsync(int id) => await _repo.DeleteAsync(id);
}