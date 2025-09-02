using FluentValidation;

namespace TaskManager.DeveloperEvaluation.Application.Users.Queries.GetUsersList;

/// <summary>
/// Validator for GetUsersListQuery
/// </summary>
public class GetUsersListValidator : AbstractValidator<GetUsersListQuery>
{
    public GetUsersListValidator()
    {
        RuleFor(x => x.Page)
            .GreaterThan(0).WithMessage("Page number must be greater than 0");

        RuleFor(x => x.Size)
            .GreaterThan(0).WithMessage("Page size must be greater than 0")
            .LessThanOrEqualTo(100).WithMessage("Page size must not exceed 100");
    }
}
