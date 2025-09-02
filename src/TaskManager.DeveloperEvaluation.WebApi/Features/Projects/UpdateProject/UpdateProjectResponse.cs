namespace TaskManager.DeveloperEvaluation.WebApi.Features.Projects.UpdateProject;

/// <summary>
/// Response DTO for successful project update.
/// </summary>
public class UpdateProjectResponse
{
    /// <summary>
    /// Gets or sets whether the operation was successful.
    /// </summary>
    public bool Success { get; set; }

    /// <summary>
    /// Gets or sets the success message.
    /// </summary>
    public string Message { get; set; } = "Project updated successfully";

    /// <summary>
    /// Gets or sets the timestamp when the response was generated.
    /// </summary>
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}