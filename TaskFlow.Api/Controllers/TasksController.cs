using Microsoft.AspNetCore.Mvc;
using TaskFlow.Api.DTOs;
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
    public async Task<ActionResult<List<TaskResponse>>> GetAll()
    {
        var tasks = await _taskService.GetAllAsync();

        var response = tasks.Select(t => new TaskResponse
        {
            Id = t.Id,
            Title = t.Title,
            Description = t.Description,
            Status = t.Status,
            DueDate = t.DueDate,
            CreatedAt = t.CreatedAt
        }).ToList();

        return Ok(response);
    }




    [HttpGet("{id:int}")]
    public async Task<ActionResult<TaskResponse>> GetById(int id)
    {
        var task = await _taskService.FindByIdAsync(id);
        if(task is null)
        {
            return NotFound();
        }

        return Ok(new TaskItem
        {
            Id = task.Id,
            Title = task.Title,
            Description = task.Description,
            Status = task.Status,
            DueDate = task.DueDate,
            CreatedAt = task.CreatedAt
        });
    }




    [HttpPost]
    public async Task<ActionResult<TaskResponse>> CreateTask(CreateTaskRequest taskItem)
    {
        var task = new TaskItem
        {
            Title = taskItem.Title,
            Description = taskItem.Description,
            DueDate = taskItem.DueDate
        };

        var created = await _taskService.CreateAsync(task);

        var response = new TaskResponse
        {
            Id = created.Id,
            Title = created.Title,
            Description = created.Description,
            Status = created.Status,
            DueDate = created.DueDate,
            CreatedAt = created.CreatedAt
        };

        return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
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
    public async Task<ActionResult> EditTaskById(int id, UpdateTaskRequest updated)
    {
        var task = new TaskItem
        {
            Title = updated.Title,
            Description = updated.Description,
            Status = updated.Status,
            DueDate = updated.DueDate
        };

        if (!await _taskService.UpdateAsync(id, task))
        {
            return NotFound();
        }

        return NoContent();
    }

}