using MediatR;
using Microsoft.Extensions.Logging;
using TaskManager.DeveloperEvaluation.Domain.Repositories;

namespace TaskManager.DeveloperEvaluation.Application.Projects.DeleteProject;

/// <summary>
/// Handles the deletion of a <see cref="Domain.Entities.Project"/> and all its associated tasks.
/// </summary>
public class DeleteProjectHandler : IRequestHandler<DeleteProjectCommand, DeleteProjectResponse>
{
    private readonly IProjectRepository _projectRepository;
    private readonly ITaskRepository _taskRepository;
    private readonly ILogger<DeleteProjectHandler> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteProjectHandler"/> class.
    /// </summary>
    /// <param name="projectRepository">
    /// The repository used to load and delete <see cref="Domain.Entities.Project"/> entities.
    /// </param>
    /// <param name="taskRepository">
    /// The repository used to delete associated <see cref="Domain.Entities.Task"/> entities.
    /// </param>
    /// <param name="logger">
    /// Used to log the deletion events.
    /// </param>
    public DeleteProjectHandler(
        IProjectRepository projectRepository,
        ITaskRepository taskRepository,
        ILogger<DeleteProjectHandler> logger)
    {
        _projectRepository = projectRepository;
        _taskRepository = taskRepository;
        _logger = logger;
    }

    /// <summary>
    /// Processes a <see cref="DeleteProjectCommand"/> by retrieving the specified project,
    /// deleting all associated tasks, and then deleting the project itself.
    /// </summary>
    /// <param name="request">
    /// The <see cref="DeleteProjectCommand"/> containing the ID of the project to delete.
    /// </param>
    /// <param name="cancellationToken">
    /// A token to observe while awaiting the database operations.
    /// </param>
    /// <returns>
    /// A <see cref="DeleteProjectResult"/> indicating whether the deletion succeeded.
    /// </returns>
    /// <exception cref="KeyNotFoundException">
    /// Thrown if no project with the specified ID exists in the repository.
    /// </exception>
    public async Task<DeleteProjectResponse> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
    {
        var project = await _projectRepository.GetByIdAsync(request.Id, cancellationToken);
        if (project == null)
            throw new KeyNotFoundException($"Project with ID '{request.Id}' not found.");

        // Log the project being deleted
        _logger.LogInformation(
            "ProjectDeleted | ProjectId={ProjectId} | Name={Name} | UserId={UserId} | Status={Status} | TaskCount={TaskCount}",
            project.Id,
            project.Name,
            project.UserId,
            project.Status,
            project.Tasks?.Count ?? 0
        );

        // Delete all associated tasks first (if any)
        if (project.Tasks != null && project.Tasks.Any())
        {
            foreach (var task in project.Tasks)
            {
                _logger.LogInformation(
                    "  TaskDeleted | ProjectId={ProjectId} | TaskId={TaskId} | TaskTitle={TaskTitle} | TaskStatus={TaskStatus}",
                    project.Id,
                    task.Id,
                    task.Title,
                    task.Status
                );
            }

            // Delete all tasks associated with this project
            await _taskRepository.DeleteAllTasksByProjectIdAsync(project.Id, cancellationToken);
        }

        // Delete the project itself
        await _projectRepository.DeleteAsync(project.Id, cancellationToken);
        await _projectRepository.SaveChangesAsync(cancellationToken);

        return new DeleteProjectResponse { Success = true };
    }
}