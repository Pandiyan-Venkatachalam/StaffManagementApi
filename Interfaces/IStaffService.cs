namespace StaffManagementApi.Interfaces;
using StaffManagementApi.Models;
public interface IStaffService {
    Task<IEnumerable<Staff>> GetStaffsAsync();
    Task AddStaffAsync(Staff staff);
    Task UpdateStaffAsync(Staff staff);
    Task DeleteStaffAsync(int id);
}