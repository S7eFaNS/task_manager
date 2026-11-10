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
    public async Task<ActionResult<List<TaskItem>>> GetAll()
    {
        return await _taskService.GetAllAsync();
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<TaskItem>> GetById(int id)
    {
        var task = await _taskService.FindByIdAsync(id);
        if(task is null)
        {
            return NotFound();
        }
        return task;
    }

    [HttpPost]
    public async Task<ActionResult<TaskItem>> CreateTask(TaskItem taskItem)
    {
        var created = await _taskService.CreateAsync(taskItem);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteTaskById(int id)
    {
        if (!await _taskService.DeleteAsync(id))
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> EditTaskById(int id, TaskItem updated)
    {
        if (!await _taskService.UpdateAsync(id, updated))
        {
            return NotFound();
        }

        return NoContent();
    }

}