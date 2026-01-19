namespace StaffManagementApi.Interfaces;
using StaffManagementApi.Models;

public interface ITaskService {
    Task<IEnumerable<UserTask>> GetTasksAsync(string username, string role);
    Task AddTaskAsync(UserTask task);
    Task UpdateTaskStatusAsync(int id, string status);
    Task DeleteTaskAsync(int id);
}
