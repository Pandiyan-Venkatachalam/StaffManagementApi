namespace StaffManagementApi.Interfaces;
using StaffManagementApi.Models;

public interface ITaskRepository {
    Task<IEnumerable<UserTask>> GetAllAsync();
    Task<IEnumerable<UserTask>> GetByUsernameAsync(string username);
    Task AddAsync(UserTask task);
    Task UpdateStatusAsync(int id, string status);
    Task DeleteAsync(int id);
}