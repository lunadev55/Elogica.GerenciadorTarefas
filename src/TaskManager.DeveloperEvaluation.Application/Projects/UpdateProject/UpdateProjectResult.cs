namespace TaskManager.DeveloperEvaluation.Application.Projects.UpdateProject;

/// <summary>
/// Represents the outcome of an <see cref="UpdateProjectCommand"/>, indicating whether the update succeeded.
/// </summary>
public class UpdateProjectResult
{
    /// <summary>
    /// Gets or sets a value indicating whether the project update operation was successful.
    /// </summary>
    public bool Success { get; set; }
}