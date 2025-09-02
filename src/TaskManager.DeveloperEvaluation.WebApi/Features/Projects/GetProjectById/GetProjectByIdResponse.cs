using TaskManager.DeveloperEvaluation.Domain.Enums;

namespace TaskManager.DeveloperEvaluation.WebApi.Features.Projects.GetProjectById;

/// <summary>
/// Response DTO for project details.
/// </summary>
public class GetProjectByIdResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public ProjectStatus Status { get; set; }
    public Guid UserId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }    
    public ICollection<Task> Tasks { get; set; }
}
