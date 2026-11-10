using System.ComponentModel.DataAnnotations;
using TaskFlow.Api.Entities;

namespace TaskFlow.Api.DTOs;

public class UpdateTaskRequest
{
    [Required]
    [MaxLength(100)]
    public string Title { get; set; } = string.Empty;

    [MaxLength(1000)]
    public string? Description { get; set; }

    public TaskItemStatus Status { get; set; }

    public DateTime? DueDate { get; set; }
}