namespace TaskManager.DeveloperEvaluation.WebApi.Features.Tasks.CreateTaskFeature;

/// <summary>
/// Response DTO for successful task creation.
/// </summary>
public class CreateTaskResponse
{
    /// <summary>
    /// Gets or sets the unique identifier of the created task.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the success message.
    /// </summary>
    public string Message { get; set; } = "Task created successfully";

    /// <summary>
    /// Gets or sets the timestamp when the response was generated.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}