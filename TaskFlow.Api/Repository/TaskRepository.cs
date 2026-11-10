using Microsoft.EntityFrameworkCore;
using TaskFlow.Api.Data;
using TaskFlow.Api.Entities;

namespace TaskFlow.Api.Repositories;

public class TaskRepository : ITaskRepository
{
    private readonly TaskFlowDbContext _db;

    public TaskRepository(TaskFlowDbContext db)
    {
        _db = db;
    }

    public async Task<List<TaskItem>> GetAllAsync(TaskItemStatus? status, string? search, string? sort)
    {
        IQueryable<TaskItem> query = _db.Tasks;

        if (status.HasValue)
        {
            query = query.Where(t => t.Status == status.Value);
        }

        if (!string.IsNullOrWhiteSpace(search))
        {
            query = query.Where(t => t.Title.Contains(search) ||
            (t.Description != null && t.Description.Contains(search)));
        }

        query = sort switch
        {
            "createdAsc" => query.OrderBy(t => t.CreatedAt),
            "createdDesc" => query.OrderByDescending(t => t.CreatedAt),
            _ => query
        };

        return await query.ToListAsync();
    }

    public async Task<TaskItem?> FindByIdAsync(int id)
    {
        return await _db.Tasks
            .FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<TaskItem> CreateAsync(TaskItem task)
    {
        task.CreatedAt = DateTime.UtcNow;

        _db.Tasks.Add(task);

        await _db.SaveChangesAsync();

        return task;
    }

        public async Task<bool> DeleteAsync(int id)
    {
        var task = await FindByIdAsync(id);

        if (task is null)
            return false;

        _db.Tasks.Remove(task);

        await _db.SaveChangesAsync();

        return true;
    }

    public async Task<bool> UpdateAsync(int id, TaskItem updated)
    {
        var task = await FindByIdAsync(id);

        if (task is null)
            return false;

        task.Title = updated.Title;
        task.Description = updated.Description;
        task.Status = updated.Status;
        task.DueDate = updated.DueDate;

        await _db.SaveChangesAsync();

        return true;
    }

}