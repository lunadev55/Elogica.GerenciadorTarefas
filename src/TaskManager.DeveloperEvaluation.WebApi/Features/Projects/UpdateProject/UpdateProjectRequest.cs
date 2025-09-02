using TaskManager.DeveloperEvaluation.Domain.Enums;

namespace TaskManager.DeveloperEvaluation.WebApi.Features.Projects.UpdateProject;

/// <summary>
/// Request DTO for updating an existing project via API.
/// </summary>
public class UpdateProjectRequest
{
    /// <summary>
    /// Gets or sets the name of the project.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the description of the project.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Gets or sets the start date of the project.
    /// </summary>
    public DateTime StartDate { get; set; }

    /// <summary>
    /// Gets or sets the optional end date of the project.
    /// </summary>
    public DateTime? EndDate { get; set; }

    /// <summary>
    /// Gets or sets the status of the project.
    /// </summary>
    public ProjectStatus Status { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier of the user who owns the project.
    /// </summary>
    public Guid UserId { get; set; }
}