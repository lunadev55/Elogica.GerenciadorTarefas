using FluentValidation;
using TaskManager.DeveloperEvaluation.Domain.Entities;

namespace TaskManager.DeveloperEvaluation.Domain.Validation;

public class UserValidator : AbstractValidator<User>
{
    public UserValidator()
    {
        RuleFor(user => user.Email).SetValidator(new EmailValidator());

        RuleFor(user => user.Username)
            .NotEmpty()
            .MinimumLength(3).WithMessage("Username must be at least 3 characters long.")
            .MaximumLength(50).WithMessage("Username cannot be longer than 50 characters.");
        
        RuleFor(user => user.Password).SetValidator(new PasswordValidator());

        RuleFor(user => user.Phone)
            .Matches(@"^\(\d{2}\)\d{5}-\d{4}$")
            .WithMessage("Phone number must be in the format (XX)XXXXX-XXXX");
  
    }
}
