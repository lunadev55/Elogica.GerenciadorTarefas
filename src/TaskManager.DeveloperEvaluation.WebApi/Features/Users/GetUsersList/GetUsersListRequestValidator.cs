using FluentValidation;

namespace TaskManager.DeveloperEvaluation.WebApi.Features.Users.GetUsersList;

/// <summary>
/// Validator for GetUsersListRequest
/// </summary>
public class GetUsersListRequestValidator : AbstractValidator<GetUsersListRequest>
{
    public GetUsersListRequestValidator()
    {
        RuleFor(x => x.Page)
            .GreaterThan(0).WithMessage("Page number must be greater than 0");

        RuleFor(x => x.Size)
            .GreaterThan(0).WithMessage("Page size must be greater than 0")
            .LessThanOrEqualTo(100).WithMessage("Page size must not exceed 100");
    }
}
