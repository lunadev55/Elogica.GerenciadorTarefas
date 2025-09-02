using MediatR;
using TaskManager.DeveloperEvaluation.Domain.Enums;

namespace TaskManager.DeveloperEvaluation.Application.Projects.UpdateProject;

/// <summary>
/// Represents a request to update an existing <see cref="Domain.Entities.Project"/>,
/// including all project fields.
/// </summary>
public class UpdateProjectCommand : IRequest<UpdateProjectResult>
{
    /// <summary>
    /// Gets or sets the unique identifier of the project to update.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the updated name of the project.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the updated description of the project.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Gets or sets the updated start date of the project.
    /// </summary>
    public DateTime StartDate { get; set; }

    /// <summary>
    /// Gets or sets the updated optional end date of the project.
    /// </summary>
    public DateTime? EndDate { get; set; }

    /// <summary>
    /// Gets or sets the updated status of the project.
    /// </summary>
    public ProjectStatus Status { get; set; }

    /// <summary>
    /// Gets or sets the updated unique identifier of the user who owns the project.
    /// </summary>
    public Guid UserId { get; set; }
}