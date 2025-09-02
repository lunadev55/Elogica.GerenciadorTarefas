using FluentValidation;
using TaskManager.DeveloperEvaluation.Domain.Enums;

namespace TaskManager.DeveloperEvaluation.Application.Projects.UpdateProject;

/// <summary>
/// Validates the <see cref="UpdateProjectCommand"/> to ensure that all required fields
/// are provided and conform to business constraints when updating a project.
/// </summary>
public class UpdateProjectValidator : AbstractValidator<UpdateProjectCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateProjectValidator"/> class,
    /// defining validation rules for <see cref="UpdateProjectCommand"/>.
    /// </summary>
    public UpdateProjectValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Project Id is required.");

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
            
        RuleFor(x => x.EndDate)
            .NotNull()
            .WithMessage("EndDate is required when Status is Completed.")
            .When(x => x.Status == ProjectStatus.Completed);
                
        RuleFor(x => x.StartDate)
            .Must((command, startDate) =>
            {                
                if (command.Status == ProjectStatus.InProgress ||
                    command.Status == ProjectStatus.Completed)
                    return true;
                                
                return startDate.Date >= DateTime.Today;
            })
            .WithMessage("StartDate cannot be in the past for projects that haven't started yet.")
            .When(x => x.Status == ProjectStatus.NotStarted);
    }
}