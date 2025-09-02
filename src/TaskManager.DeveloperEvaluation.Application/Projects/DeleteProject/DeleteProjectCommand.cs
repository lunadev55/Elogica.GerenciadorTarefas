using MediatR;

namespace TaskManager.DeveloperEvaluation.Application.Projects.DeleteProject;

/// <summary>
/// Command to request deletion of a specific <see cref="Domain.Entities.Project"/>.
/// </summary>
public class DeleteProjectCommand : IRequest<DeleteProjectResponse>
{
    /// <summary>
    /// Gets or sets the unique identifier of the project to delete.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Initializes a new instance of DeleteUserCommand
    /// </summary>
    /// <param name="id">The ID of the user to delete</param>
    public DeleteProjectCommand(Guid id)
    {
        Id = id;
    }
}