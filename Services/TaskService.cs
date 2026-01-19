using StaffManagementApi.Interfaces;
using StaffManagementApi.Models;
using RestSharp; 
using Microsoft.Extensions.Logging; 

namespace StaffManagementApi.Services;

public class TaskService : ITaskService {
    private readonly ITaskRepository _repo;
    private readonly ILogger<TaskService> _logger; 

    public TaskService(ITaskRepository repo, ILogger<TaskService> logger) {
        _repo = repo;
        _logger = logger;
    }

    public async Task<IEnumerable<UserTask>> GetTasksAsync(string username, string role) {
    // Inga Role name-ah trim panni lowercase-la check pannunga
    if (role.Trim().ToLower() == "admin") {
        return await _repo.GetAllAsync();
    }
    
    return await _repo.GetByUsernameAsync(username);
    }

    // public async Task<IEnumerable<UserTask>> GetTasksAsync(string username, string role) {
    // if (role.Equals("admin", StringComparison.OrdinalIgnoreCase)) {
    //     return await _repo.GetAllAsync();
    // }
    
    // return await _repo.GetByUsernameAsync(username);
    // }

    public async Task AddTaskAsync(UserTask task) {
        await _repo.AddAsync(task);
        _logger.LogInformation("Task added to database with ID: {Id}", task.Id);

        try {
            var client = new RestClient("http://localhost:5176");
            var request = new RestRequest("/notify", Method.Post);
            request.AddJsonBody(new {
                Title = "New Task Assigned",
                TaskTitle = task.Title,
                User = task.AssignedTo
            });

            var response = await client.ExecuteAsync(request);

            if (response.IsSuccessful) {
                _logger.LogInformation("Notification successfully sent via RestSharp.");
            } else {
                _logger.LogWarning("RestSharp alert failed: {Status}", response.StatusCode);
            }
        }
        catch (Exception ex) {
            _logger.LogError(ex, "RestSharp calling error");
            
        }
    }

    public async Task UpdateTaskStatusAsync(int id, string status) {
        _logger.LogInformation("Updating status for Task {Id} to {Status}", id, status);
        await _repo.UpdateStatusAsync(id, status);
    }

    public async Task DeleteTaskAsync(int id) {
        _logger.LogInformation("Deleting task with ID: {Id}", id);
        await _repo.DeleteAsync(id);
    }
}