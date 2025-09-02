namespace TaskManager.DeveloperEvaluation.WebApi.Features.Tasks.DeleteTaskFeature;

/// <summary>
/// Response DTO for successful task deletion.
/// </summary>
public class DeleteTaskResponse
{
    /// <summary>
    /// Gets or sets whether the operation was successful.
    /// </summary>
    public bool Success { get; set; }

    /// <summary>
    /// Gets or sets the success message.
    /// </summary>
    public string Message { get; set; } = "Task deleted successfully";

    /// <summary>
    /// Gets or sets the timestamp when the response was generated.
    /// </summary>
    public DateTime DeletedAt { get; set; } = DateTime.UtcNow;
}