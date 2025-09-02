namespace TaskManager.DeveloperEvaluation.WebApi.Features.Projects.CreateProjectFeature;

/// <summary>
/// Response DTO for successful project creation.
/// </summary>
public class CreateProjectResponse
{
    /// <summary>
    /// Gets or sets the unique identifier of the created project.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the success message.
    /// </summary>
    public string Message { get; set; } = "Project created successfully";

    /// <summary>
    /// Gets or sets the timestamp when the response was generated.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}