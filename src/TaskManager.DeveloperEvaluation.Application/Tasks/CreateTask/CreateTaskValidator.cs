using FluentValidation;
using TaskManager.DeveloperEvaluation.Domain.Enums;
using TaskStatus = TaskManager.DeveloperEvaluation.Domain.Enums.TaskStatus;

namespace TaskManager.DeveloperEvaluation.Application.Tasks.CreateTask
{
    /// <summary>
    /// Validates the <see cref="CreateTaskCommand"/> to ensure all required fields
    /// and constraints for creating a task are met.
    /// </summary>
    public class CreateTaskValidator : AbstractValidator<CreateTaskCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateTaskValidator"/> class
        /// and defines validation rules for <see cref="CreateTaskCommand"/>.
        /// </summary>
        public CreateTaskValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .WithMessage("Title is required.")
                .MaximumLength(100)
                .WithMessage("Title must not exceed 200 characters.");

            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage("Description is required.")
                .MaximumLength(500)
                .WithMessage("Description must not exceed 1000 characters.");

            RuleFor(x => x.DueDate)
                .NotEmpty()
                .WithMessage("DueDate is required.")
                .Must(dueDate => dueDate.Date >= DateTime.Today)
                .WithMessage("DueDate cannot be in the past.");

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
                .Must((command, dueDate) => dueDate.Date <= DateTime.Today.AddDays(7))
                .WithMessage("High priority tasks should have due date within the next 7 days.")
                .When(x => x.Priority == TaskPriority.High);
                    
            RuleFor(x => x.DueDate)
                .Must((command, dueDate) => dueDate.Date <= DateTime.Today.AddDays(3))
                .WithMessage("Critical priority tasks should have due date within the next 3 days.")
                .When(x => x.Priority == TaskPriority.Critical);

            RuleFor(x => x.Status)
                .NotEqual(TaskStatus.Done)
                .WithMessage("New tasks cannot be created with Completed status. Use Pending or InProgress instead.");
        }
    }
}