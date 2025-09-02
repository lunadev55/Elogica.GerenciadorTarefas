namespace TaskManager.DeveloperEvaluation.Application.Tasks.DeleteTask;

/// <summary>
/// Represents the outcome of a <see cref="DeleteTaskCommand"/>, indicating whether the deletion succeeded.
/// </summary>
public class DeleteTaskResult
{
    /// <summary>
    /// Gets or sets a value indicating whether the task deletion was successful.
    /// </summary>
    public bool Success { get; set; }
}