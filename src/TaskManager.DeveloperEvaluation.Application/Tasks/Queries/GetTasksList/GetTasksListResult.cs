namespace TaskManager.DeveloperEvaluation.Application.Tasks.Queries.GetTasksList;

public class GetTasksListResult
{
    public IEnumerable<TaskListItem> Data { get; set; }
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public int TotalItems { get; set; }
}

public class TaskListItem
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Status { get; set; }
    public string Priority { get; set; }
    public DateTime DueDate { get; set; }
}
