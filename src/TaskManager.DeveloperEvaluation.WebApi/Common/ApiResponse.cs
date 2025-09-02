using TaskManager.DeveloperEvaluation.Common.Validation;

namespace TaskManager.DeveloperEvaluation.WebApi.Common;

public class ApiResponse
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public IEnumerable<ValidationErrorDetail> Errors { get; set; } = [];
}
