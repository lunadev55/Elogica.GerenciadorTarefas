using FluentValidation;
using TaskManager.DeveloperEvaluation.Domain.Enums;

namespace TaskManager.DeveloperEvaluation.Application.Projects.CreateProject;

/// <summary>
/// Validates the <see cref="CreateProjectCommand"/> to ensure all required fields
/// and constraints for creating a project are met.
/// </summary>
public class CreateProjectValidator : AbstractValidator<CreateProjectCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateProjectValidator"/> class
    /// and defines validation rules for <see cref="CreateProjectCommand"/>.
    /// </summary>
    public CreateProjectValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name is required.")
            .MaximumLength(100)
            .WithMessage("Name must not exceed 200 characters.");

        RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage("Description is required.")
            .MaximumLength(500)
            .WithMessage("Description must not exceed 1000 characters.");

        RuleFor(x => x.StartDate)
            .NotEmpty()
            .WithMessage("StartDate is required.");

        RuleFor(x => x.EndDate)
            .Must((command, endDate) => !endDate.HasValue || endDate.Value >= command.StartDate)
            .WithMessage("EndDate must be greater than or equal to StartDate.")
            .When(x => x.EndDate.HasValue);

        RuleFor(x => x.Status)
            .IsInEnum()
            .WithMessage("Status must be a valid ProjectStatus value.");

        RuleFor(x => x.UserId)
            .NotEmpty()
            .WithMessage("UserId is required.");

        // Business rule: Start date should not be in the past for new projects
        RuleFor(x => x.StartDate)
            .Must(startDate => startDate.Date >= DateTime.Today)
            .WithMessage("StartDate cannot be in the past.")
            .When(x => x.Status == ProjectStatus.NotStarted || x.Status == ProjectStatus.InProgress);
    }
}