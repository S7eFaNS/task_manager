using Microsoft.AspNetCore.Mvc;
using TaskFlow.Api.Entities;
using TaskFlow.Api.Services;

namespace TaskFlow.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
    private readonly ITaskService _taskService;

    public TasksController(ITaskService taskService)
    {
        _taskService = taskService;
    }


    [HttpGet]
    public ActionResult<List<TaskItem>> GetAll()
    {
        return _taskService.GetAll();
    }

    [HttpGet("{id:int}")]
    public ActionResult<TaskItem> GetById(int id)
    {
        var task = _taskService.FindItemById(id);
        if(task is null)
        {
            return NotFound();
        }
        return task;
    }

    [HttpPost]
    public ActionResult<TaskItem> CreateTask(TaskItem taskItem)
    {
        var created = _taskService.CreateItem(taskItem);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpDelete("{id:int}")]
    public ActionResult DeleteTaskById(int id)
    {
        if (!_taskService.DeleteItemById(id))
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpPut("{id:int}")]
    public ActionResult EditTaskById(int id, TaskItem updated)
    {
        if (!_taskService.UpdateItemById(id, updated))
        {
            return NotFound();
        }

        return NoContent();
    }

}