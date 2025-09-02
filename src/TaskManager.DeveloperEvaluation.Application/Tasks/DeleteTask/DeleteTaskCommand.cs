using MediatR;

namespace TaskManager.DeveloperEvaluation.Application.Tasks.DeleteTask;

/// <summary>
/// Command to request deletion of a specific <see cref="Domain.Entities.Task"/>.
/// </summary>
public class DeleteTaskCommand : IRequest<DeleteTaskResult>
{
    /// <summary>
    /// Gets or sets the unique identifier of the task to delete.
    /// </summary>
    public Guid Id { get; set; }
}
