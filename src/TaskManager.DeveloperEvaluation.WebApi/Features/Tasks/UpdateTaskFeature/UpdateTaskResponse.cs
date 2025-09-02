namespace TaskManager.DeveloperEvaluation.WebApi.Features.Tasks.UpdateTaskFeature;

/// <summary>
/// Response DTO for successful task update.
/// </summary>
public class UpdateTaskResponse
{
    /// <summary>
    /// Gets or sets whether the operation was successful.
    /// </summary>
    public bool Success { get; set; }

    /// <summary>
    /// Gets or sets the success message.
    /// </summary>
    public string Message { get; set; } = "Task updated successfully";

    /// <summary>
    /// Gets or sets the timestamp when the response was generated.
    /// </summary>
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}