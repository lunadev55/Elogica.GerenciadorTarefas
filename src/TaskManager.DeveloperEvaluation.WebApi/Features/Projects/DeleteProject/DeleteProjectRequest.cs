namespace TaskManager.DeveloperEvaluation.WebApi.Features.Projects.DeleteProject;

/// <summary>
/// Request model for deleting a user
/// </summary>
public class DeleteProjectRequest
{
    /// <summary>
    /// The unique identifier of the user to delete
    /// </summary>
    public Guid Id { get; set; }
}
