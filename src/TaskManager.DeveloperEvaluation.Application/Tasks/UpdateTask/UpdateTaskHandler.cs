using MediatR;
using Microsoft.Extensions.Logging;
using TaskManager.DeveloperEvaluation.Domain.Repositories;
using Task = TaskManager.DeveloperEvaluation.Domain.Entities.Task;

namespace TaskManager.DeveloperEvaluation.Application.Tasks.UpdateTask;

/// <summary>
/// Handles the <see cref="UpdateTaskCommand"/> by updating all task fields
/// and persisting the changes to the database.
/// </summary>
public class UpdateTaskHandler : IRequestHandler<UpdateTaskCommand, UpdateTaskResult>
{
    private readonly ITaskRepository _taskRepository;
    private readonly IProjectRepository _projectRepository;
    private readonly IUserRepository _userRepository;
    private readonly ILogger<UpdateTaskHandler> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateTaskHandler"/> class.
    /// </summary>
    /// <param name="taskRepository">
    /// The <see cref="ITaskRepository"/> used to load and save task data.
    /// </param>
    /// <param name="projectRepository">
    /// The <see cref="IProjectRepository"/> used to validate project existence.
    /// </param>
    /// <param name="userRepository">
    /// The <see cref="IUserRepository"/> used to validate user existence.
    /// </param>
    /// <param name="logger">
    /// The logger for recording task update events.
    /// </param>
    public UpdateTaskHandler(
        ITaskRepository taskRepository,
        IProjectRepository projectRepository,
        IUserRepository userRepository,
        ILogger<UpdateTaskHandler> logger)
    {
        _taskRepository = taskRepository;
        _projectRepository = projectRepository;
        _userRepository = userRepository;
        _logger = logger;
    }

    /// <summary>
    /// Processes the update request by:
    /// 1. Loading the existing <see cref="Task"/> entity.
    /// 2. Validating that the project and user exist.
    /// 3. Updating all task fields using the domain method.
    /// 4. Persisting the changes to the database.
    /// </summary>
    /// <param name="request">
    /// The <see cref="UpdateTaskCommand"/> containing the task ID and new field values.
    /// </param>
    /// <param name="cancellationToken">
    /// A token to observe for cancellation requests.
    /// </param>
    /// <returns>
    /// An <see cref="UpdateTaskResult"/> indicating success or failure.
    /// </returns>
    /// <exception cref="KeyNotFoundException">
    /// Thrown if no task with the specified ID exists, or if the project or user is not found.
    /// </exception>
    public async Task<UpdateTaskResult> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
    {
        var task = await _taskRepository.GetByIdAsync(request.Id, cancellationToken);
        if (task == null)
            throw new KeyNotFoundException($"Task with ID '{request.Id}' not found.");

        var project = await _projectRepository.GetByIdAsync(request.ProjectId, cancellationToken);
        if (project == null)
            throw new KeyNotFoundException($"Project with ID '{request.ProjectId}' not found.");

        var user = await _userRepository.GetByIdAsync(request.UserId);
        if (user == null)
            throw new KeyNotFoundException($"User with ID '{request.UserId}' not found.");
                
        task.Update(
            request.Title,
            request.Description,
            request.DueDate,
            request.Status,
            request.Priority,
            DateTime.UtcNow
        );
    
        if (task.ProjectId != request.ProjectId || task.UserId != request.UserId)
        {            
            var updatedTask = new Task(
                task.Id,
                task.Title,
                task.Description,
                task.DueDate,
                task.Status,
                task.Priority,
                request.ProjectId,
                request.UserId,
                task.CreatedAt
            );
            updatedTask.Update(
                request.Title,
                request.Description,
                request.DueDate,
                request.Status,
                request.Priority,
                DateTime.UtcNow
            );
            task = updatedTask;
        }

        _taskRepository.Update(task);
        await _taskRepository.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "TaskModified | TaskId={TaskId} | Title={Title} | ProjectId={ProjectId} | UserId={UserId} | Status={Status} | Priority={Priority} | DueDate={DueDate}",
            task.Id,
            task.Title,
            task.ProjectId,
            task.UserId,
            task.Status,
            task.Priority,
            task.DueDate
        );

        return new UpdateTaskResult { Success = true };
    }
}