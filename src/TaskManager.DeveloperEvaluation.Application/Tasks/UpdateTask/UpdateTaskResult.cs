namespace TaskManager.DeveloperEvaluation.Application.Tasks.UpdateTask;

/// <summary>
/// Represents the outcome of an <see cref="UpdateTaskCommand"/>, indicating whether the update succeeded.
/// </summary>
public class UpdateTaskResult
{
    /// <summary>
    /// Gets or sets a value indicating whether the task update operation was successful.
    /// </summary>
    public bool Success { get; set; }
}