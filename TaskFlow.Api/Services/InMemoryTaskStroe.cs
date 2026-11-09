using TaskFlow.Api.Entities;

namespace TaskFlow.Api.Services;

public class InMemoryTaskStore
{
    private readonly List<TaskItem> _tasks = new();
    private readonly object _lock = new();
    private int _nextId = 1;



    public TaskItem Add(TaskItem task)
    {
        lock (_lock)
        {
            task.Id = _nextId++;
            task.CreatedAt = DateTime.UtcNow;
            _tasks.Add(task);
            return task;
        }
    }

    public List<TaskItem> GetAll()
    {
        lock (_lock)
        {
            return _tasks.ToList();
        }
    }

    public TaskItem? Find(int id)
    {
        lock (_lock)
        {
            return _tasks.FirstOrDefault (t => t.Id == id);
        }
    }

    public bool Delete(int id)
    {
        
        lock (_lock)
        {
            var task = Find(id);
            
            if (task is null)
            {
                return false;
            }

            return _tasks.Remove(task);
        }
    }

    public bool Update(int id, TaskItem updated)
    {
    
        lock (_lock)
        {
            var task = Find(id);

            if (task is null)
            {
                return false;
            }

            task.Title = updated.Title;
            task.Description = updated.Description;
            task.Status = updated.Status;
            task.DueDate = updated.DueDate;

            return true;
        }
        
    }



}