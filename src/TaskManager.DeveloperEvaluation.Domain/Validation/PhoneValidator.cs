using FluentValidation;

namespace TaskManager.DeveloperEvaluation.Domain.Validation;

public class PhoneValidator : AbstractValidator<string>
{
    public PhoneValidator()
    {
        RuleFor(phone => phone)
            .Matches(@"^\(\d{2}\)\d{5}-\d{4}$")
            .WithMessage("Phone number must be in the format (XX)XXXXX-XXXX");
    }
}
