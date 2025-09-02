using MediatR;
using Microsoft.Extensions.Logging;
using TaskManager.DeveloperEvaluation.Domain.Entities;
using TaskManager.DeveloperEvaluation.Domain.Repositories;

namespace TaskManager.DeveloperEvaluation.Application.Projects.UpdateProject;

/// <summary>
/// Handles the <see cref="UpdateProjectCommand"/> by updating all project fields
/// and persisting the changes to the database.
/// </summary>
public class UpdateProjectHandler : IRequestHandler<UpdateProjectCommand, UpdateProjectResult>
{
    private readonly IProjectRepository _projectRepository;
    private readonly IUserRepository _userRepository;
    private readonly ILogger<UpdateProjectHandler> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateProjectHandler"/> class.
    /// </summary>
    /// <param name="projectRepository">
    /// The <see cref="IProjectRepository"/> used to load and save project data.
    /// </param>
    /// <param name="userRepository">
    /// The <see cref="IUserRepository"/> used to validate user existence.
    /// </param>
    /// <param name="logger">
    /// The logger for recording project update events.
    /// </param>
    public UpdateProjectHandler(
        IProjectRepository projectRepository,
        IUserRepository userRepository,
        ILogger<UpdateProjectHandler> logger)
    {
        _projectRepository = projectRepository;
        _userRepository = userRepository;
        _logger = logger;
    }

    /// <summary>
    /// Processes the update request by:
    /// 1. Loading the existing <see cref="Project"/> entity.
    /// 2. Validating that the new user exists.
    /// 3. Updating all project fields using the domain method.
    /// 4. Persisting the changes to the database.
    /// </summary>
    /// <param name="request">
    /// The <see cref="UpdateProjectCommand"/> containing the project ID and new field values.
    /// </param>
    /// <param name="cancellationToken">
    /// A token to observe for cancellation requests.
    /// </param>
    /// <returns>
    /// An <see cref="UpdateProjectResult"/> indicating success or failure.
    /// </returns>
    /// <exception cref="KeyNotFoundException">
    /// Thrown if no project with the specified ID exists or if the user is not found.
    /// </exception>
    public async Task<UpdateProjectResult> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
    {
        var project = await _projectRepository.GetByIdAsync(request.Id, cancellationToken);
        if (project == null)
            throw new KeyNotFoundException($"Projeto com o ID '{request.Id}' não encontrado!.");

        var user = await _userRepository.GetByIdAsync(request.UserId);
        if (user == null)
            throw new KeyNotFoundException($"User with ID '{request.UserId}' not found.");
                
        project.Update(
            request.Name,
            request.Description,
            request.StartDate,
            request.EndDate,
            request.Status,
            DateTime.UtcNow
        );
                
        if (project.UserId != request.UserId)
        {           
            var updatedProject = new Project(
                project.Id,
                project.Name,
                project.Description,
                project.StartDate,
                project.EndDate,
                project.Status,
                request.UserId,
                project.CreatedAt
            );
            updatedProject.Update(
                request.Name,
                request.Description,
                request.StartDate,
                request.EndDate,
                request.Status,
                DateTime.UtcNow
            );
            project = updatedProject;
        }

        _projectRepository.Update(project);
        await _projectRepository.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "ProjectModified | ProjectId={ProjectId} | Name={Name} | UserId={UserId} | Status={Status} | StartDate={StartDate} | EndDate={EndDate}",
            project.Id,
            project.Name,
            project.UserId,
            project.Status,
            project.StartDate,
            project.EndDate
        );

        return new UpdateProjectResult { Success = true };
    }
}
