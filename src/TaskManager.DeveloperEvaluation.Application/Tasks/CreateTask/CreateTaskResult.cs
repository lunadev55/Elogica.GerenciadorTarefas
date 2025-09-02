namespace TaskManager.DeveloperEvaluation.Application.Tasks.CreateTask;

/// <summary>
/// Represents the outcome of a <see cref="CreateTaskCommand"/>, 
/// containing the unique identifier of the newly created task.
/// </summary>
public class CreateTaskResult
{
    /// <summary>
    /// Gets or sets the unique identifier of the task that was created.
    /// </summary>
    public Guid Id { get; set; }
}