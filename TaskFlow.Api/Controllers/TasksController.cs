using Microsoft.AspNetCore.Mvc;
using TaskFlow.Api.Entities;

namespace TaskFlow.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
    private static readonly List<TaskItem> Tasks = new();
    private static int _nextId = 1;

    private TaskItem? FindItemById(int id)
    {
        return Tasks.FirstOrDefault (t => t.Id == id);
    }

    [HttpGet]
    public ActionResult<List<TaskItem>> GetAll()
    {
        return Tasks;
    }

    [HttpGet("{id:int}")]
    public ActionResult<TaskItem> GetById(int id)
    {
        var task = FindItemById(id);

        if (task is null)
        {
            return NotFound();
        }

        return task;
    }

    [HttpPost]
    public ActionResult<TaskItem> CreateTask(TaskItem taskItem)
    {
        taskItem.Id = _nextId++;
        taskItem.CreatedAt = DateTime.UtcNow;

        Tasks.Add(taskItem);

        return CreatedAtAction(nameof(GetById), new {id = taskItem.Id}, taskItem);
    }

    [HttpDelete("{id:int}")]
    public ActionResult DeleteTaskById(int id)
    {
        var task = FindItemById(id);

        if (task is null)
        {
            return NotFound();
        }

        Tasks.Remove(task);
        return NoContent();
    }

    [HttpPut("{id:int}")]
    public ActionResult EditTaskById(int id, TaskItem updated)
    {
        var task = FindItemById(id);

        if (task is null)
        {
            return NotFound();
        }

        task.Title = updated.Title;
        task.Description = updated.Description;
        task.Status = updated.Status;
        task.DueDate = updated.DueDate;

        return NoContent();
    }


}