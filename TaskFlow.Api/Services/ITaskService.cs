using TaskFlow.Api.Entities;

namespace TaskFlow.Api.Services;

public interface ITaskService
{
    List<TaskItem> GetAll();
    TaskItem? FindItemById(int id);
    TaskItem CreateItem(TaskItem task);
    bool DeleteItemById(int id);
    bool UpdateItemById(int id, TaskItem updated);
}