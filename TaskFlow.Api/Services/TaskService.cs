using TaskFlow.Api.Entities;

namespace TaskFlow.Api.Services;

public class TaskService : ITaskService
{
    private readonly InMemoryTaskStore _store;

    public TaskService(InMemoryTaskStore store)
    {
        _store = store;
    }

    public TaskItem CreateItem(TaskItem task)
    {
        return _store.Add(task);
    }

    public bool DeleteItemById(int id)
    {
        return _store.Delete(id);
    }

    public TaskItem? FindItemById(int id)
    {
        return _store.Find(id);
    }

    public List<TaskItem> GetAll()
    {
        return _store.GetAll();
    }

    public bool UpdateItemById(int id, TaskItem updated)
    {
        return _store.Update(id, updated);
    }

}