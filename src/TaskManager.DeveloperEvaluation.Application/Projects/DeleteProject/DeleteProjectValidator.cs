using FluentValidation;

namespace TaskManager.DeveloperEvaluation.Application.Projects.DeleteProject;

/// <summary>
/// Validates the <see cref="DeleteProjectCommand"/> to ensure it contains a non-empty project identifier.
/// </summary>
public class DeleteProjectValidator : AbstractValidator<DeleteProjectCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteProjectValidator"/> class
    /// and defines rules for <see cref="DeleteProjectCommand"/>.
    /// </summary>
    public DeleteProjectValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Project Id is required.");
    }
}