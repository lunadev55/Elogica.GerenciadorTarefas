namespace TaskManager.DeveloperEvaluation.Application.Projects.CreateProject;

/// <summary>
/// Represents the outcome of a <see cref="CreateProjectCommand"/>, 
/// containing the unique identifier of the newly created project.
/// </summary>
public class CreateProjectResult
{
    /// <summary>
    /// Gets or sets the unique identifier of the project that was created.
    /// </summary>
    public Guid Id { get; set; }
}