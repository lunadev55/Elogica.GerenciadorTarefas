using FluentValidation;
using TaskManager.DeveloperEvaluation.Domain.Enums;
using TaskStatus = TaskManager.DeveloperEvaluation.Domain.Enums.TaskStatus;

namespace TaskManager.DeveloperEvaluation.Application.Tasks.UpdateTask;

/// <summary>
/// Validates the <see cref="UpdateTaskCommand"/> to ensure that all required fields
/// are provided and conform to business constraints when updating a task.
/// </summary>
public class UpdateTaskValidator : AbstractValidator<UpdateTaskCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateTaskValidator"/> class,
    /// defining validation rules for <see cref="UpdateTaskCommand"/>.
    /// </summary>
    public UpdateTaskValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Task Id is required.");

        RuleFor(x => x.Title)
            .NotEmpty()
            .WithMessage("Title is required.")
            .MaximumLength(200)
            .WithMessage("Title must not exceed 200 characters.");

        RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage("Description is required.")
            .MaximumLength(1000)
            .WithMessage("Description must not exceed 1000 characters.");

        RuleFor(x => x.DueDate)
            .NotEmpty()
            .WithMessage("DueDate is required.");

        RuleFor(x => x.Status)
            .IsInEnum()
            .WithMessage("Status must be a valid TaskStatus value.");

        RuleFor(x => x.Priority)
            .IsInEnum()
            .WithMessage("Priority must be a valid TaskPriority value.");

        RuleFor(x => x.ProjectId)
            .NotEmpty()
            .WithMessage("ProjectId is required.");

        RuleFor(x => x.UserId)
            .NotEmpty()
            .WithMessage("UserId is required.");
                
        RuleFor(x => x.DueDate)
            .Must((command, dueDate) =>
            {                
                if (command.Status == TaskStatus.Done)
                    return true;
                                
                return dueDate.Date >= DateTime.Today;
            })
            .WithMessage("DueDate cannot be in the past for tasks that are not completed.")
            .When(x => x.Status == TaskStatus.InProgress);
                
        RuleFor(x => x.DueDate)
            .Must((command, dueDate) =>
            {                
                if (command.Priority == TaskPriority.High)
                    return dueDate.Date <= DateTime.Today.AddDays(30);

                return true;
            })
            .WithMessage("High priority tasks should have a due date within 30 days.")
            .When(x => x.Priority == TaskPriority.High && x.Status != TaskStatus.Done);
    }
}