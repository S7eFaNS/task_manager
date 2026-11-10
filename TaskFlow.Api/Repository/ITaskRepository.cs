using TaskFlow.Api.Entities;

namespace TaskFlow.Api.Repositories;

public interface ITaskRepository
{
    Task<List<TaskItem>> GetAllAsync(TaskItemStatus? status, string? search, string? sort);

    Task<TaskItem?> FindByIdAsync(int id);

    Task<TaskItem> CreateAsync(TaskItem task);

    Task<bool> DeleteAsync(int id);

    Task<bool> UpdateAsync(int id, TaskItem updated);

}
