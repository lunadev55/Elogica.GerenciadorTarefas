using MediatR;
using TaskManager.DeveloperEvaluation.Domain.Enums;
using TaskStatus = TaskManager.DeveloperEvaluation.Domain.Enums.TaskStatus;

namespace TaskManager.DeveloperEvaluation.Application.Tasks.CreateTask;

/// <summary>
/// Represents a request to create a new <see cref="Domain.Entities.Task"/>.
/// </summary>
public class CreateTaskCommand : IRequest<CreateTaskResult>
{
    /// <summary>
    /// Gets or sets the title of the task.
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// Gets or sets the description of the task.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Gets or sets the due date of the task.
    /// </summary>
    public DateTime DueDate { get; set; }

    /// <summary>
    /// Gets or sets the status of the task.
    /// </summary>
    public TaskStatus Status { get; set; }

    /// <summary>
    /// Gets or sets the priority of the task.
    /// </summary>
    public TaskPriority Priority { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier of the project this task belongs to.
    /// </summary>
    public Guid ProjectId { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier of the user assigned to this task.
    /// </summary>
    public Guid UserId { get; set; }
}