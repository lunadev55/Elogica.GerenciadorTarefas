using MediatR;
using TaskManager.DeveloperEvaluation.Domain.Enums;
using TaskStatus = TaskManager.DeveloperEvaluation.Domain.Enums.TaskStatus;

namespace TaskManager.DeveloperEvaluation.Application.Tasks.UpdateTask;

/// <summary>
/// Represents a request to update an existing <see cref="Domain.Entities.Task"/>,
/// including all task fields.
/// </summary>
public class UpdateTaskCommand : IRequest<UpdateTaskResult>
{
    /// <summary>
    /// Gets or sets the unique identifier of the task to update.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the updated title of the task.
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// Gets or sets the updated description of the task.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Gets or sets the updated due date of the task.
    /// </summary>
    public DateTime DueDate { get; set; }

    /// <summary>
    /// Gets or sets the updated status of the task.
    /// </summary>
    public TaskStatus Status { get; set; }

    /// <summary>
    /// Gets or sets the updated priority of the task.
    /// </summary>
    public TaskPriority Priority { get; set; }

    /// <summary>
    /// Gets or sets the updated unique identifier of the project this task belongs to.
    /// </summary>
    public Guid ProjectId { get; set; }

    /// <summary>
    /// Gets or sets the updated unique identifier of the user assigned to this task.
    /// </summary>
    public Guid UserId { get; set; }
}