namespace TaskManager.DeveloperEvaluation.WebApi.Features.Projects.GetProjectsList;

/// <summary>
/// Request DTO for retrieving a paginated list of projects
/// </summary>
public class GetProjectsListRequest
{
    /// <summary>
    /// Gets or sets the page number (1-based)
    /// </summary>
    public int Page { get; set; }

    /// <summary>
    /// Gets or sets the page size
    /// </summary>
    public int Size { get; set; }
}
