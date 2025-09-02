using MediatR;
using Microsoft.Extensions.Logging;
using TaskManager.DeveloperEvaluation.Domain.Repositories;

namespace TaskManager.DeveloperEvaluation.Application.Tasks.DeleteTask;

/// <summary>
/// Handles the deletion of a <see cref="Domain.Entities.Task"/>.
/// </summary>
public class DeleteTaskHandler : IRequestHandler<DeleteTaskCommand, DeleteTaskResult>
{
    private readonly ITaskRepository _taskRepository;
    private readonly ILogger<DeleteTaskHandler> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteTaskHandler"/> class.
    /// </summary>
    /// <param name="taskRepository">
    /// The repository used to load and delete <see cref="Domain.Entities.Task"/> entities.
    /// </param>
    /// <param name="logger">
    /// Used to log the deletion events.
    /// </param>
    public DeleteTaskHandler(
        ITaskRepository taskRepository,
        ILogger<DeleteTaskHandler> logger)
    {
        _taskRepository = taskRepository;
        _logger = logger;
    }

    /// <summary>
    /// Processes a <see cref="DeleteTaskCommand"/> by retrieving the specified task
    /// and then deleting it.
    /// </summary>
    /// <param name="request">
    /// The <see cref="DeleteTaskCommand"/> containing the ID of the task to delete.
    /// </param>
    /// <param name="cancellationToken">
    /// A token to observe while awaiting the database operations.
    /// </param>
    /// <returns>
    /// A <see cref="DeleteTaskResult"/> indicating whether the deletion succeeded.
    /// </returns>
    /// <exception cref="KeyNotFoundException">
    /// Thrown if no task with the specified ID exists in the repository.
    /// </exception>
    public async Task<DeleteTaskResult> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
    {
        var task = await _taskRepository.GetByIdAsync(request.Id, cancellationToken);
        if (task == null)
            throw new KeyNotFoundException($"Task with ID '{request.Id}' not found.");
                
        _logger.LogInformation(
            "TaskDeleted | TaskId={TaskId} | Title={Title} | ProjectId={ProjectId} | UserId={UserId} | Status={Status} | Priority={Priority}",
            task.Id,
            task.Title,
            task.ProjectId,
            task.UserId,
            task.Status,
            task.Priority
        );
                
        await _taskRepository.DeleteAsync(task.Id, cancellationToken);
        await _taskRepository.SaveChangesAsync(cancellationToken);

        return new DeleteTaskResult { Success = true };
    }
}