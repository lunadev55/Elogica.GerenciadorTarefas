using FluentValidation;

namespace TaskManager.DeveloperEvaluation.Application.Tasks.DeleteTask;

/// <summary>
/// Validates the <see cref="DeleteTaskCommand"/> to ensure it contains a non-empty task identifier.
/// </summary>
public class DeleteTaskValidator : AbstractValidator<DeleteTaskCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteTaskValidator"/> class
    /// and defines rules for <see cref="DeleteTaskCommand"/>.
    /// </summary>
    public DeleteTaskValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Task Id is required.");
    }
}