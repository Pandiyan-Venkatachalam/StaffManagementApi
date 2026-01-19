namespace StaffManagementApi.Interfaces;
using StaffManagementApi.Models;

public interface IStaffRepository {
    Task<IEnumerable<Staff>> GetAllAsync();
    Task<Staff?> GetByIdAsync(int id);
    Task AddAsync(Staff staff);
    Task UpdateAsync(Staff staff);
    Task DeleteAsync(int id);
}