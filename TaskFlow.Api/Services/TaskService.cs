using TaskFlow.Api.Entities;
using TaskFlow.Api.Repositories;

namespace TaskFlow.Api.Services;

public class TaskService : ITaskService
{
    private readonly ITaskRepository _repository;

    public TaskService(ITaskRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<TaskItem>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<TaskItem?> FindByIdAsync(int id)
    {
        return await _repository.FindByIdAsync(id);
    }

    public async Task<TaskItem> CreateAsync(TaskItem task)
    {
        return await _repository.CreateAsync(task);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _repository.DeleteAsync(id);
    }

    public async Task<bool> UpdateAsync(int id, TaskItem updated)
    {
        return await _repository.UpdateAsync(id, updated);
    }

}