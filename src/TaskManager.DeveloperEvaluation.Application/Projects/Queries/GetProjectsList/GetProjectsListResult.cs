namespace TaskManager.DeveloperEvaluation.Application.Projects.Queries.GetProjectsList;

public class GetProjectsListResult
{
    public IEnumerable<ProjectListItem> Data { get; set; }
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public int TotalItems { get; set; }
}

public class ProjectListItem
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Status { get; set; }
    public DateTime StartDate { get; set; }       
    public DateTime? EndDate { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public Guid UserId { get; set; }
}
