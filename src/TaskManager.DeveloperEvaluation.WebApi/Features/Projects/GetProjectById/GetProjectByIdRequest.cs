namespace TaskManager.DeveloperEvaluation.WebApi.Features.Projects.GetProjectById;

/// <summary>
/// Request DTO for retrieving a project by its ID.
/// </summary>
public class GetProjectByIdRequest
{
    /// <summary>
    /// Gets or sets the unique identifier of the project.
    /// </summary>
    public Guid Id { get; set; }
}
