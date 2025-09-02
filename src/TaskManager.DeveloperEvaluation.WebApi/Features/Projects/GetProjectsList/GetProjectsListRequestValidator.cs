using FluentValidation;

namespace TaskManager.DeveloperEvaluation.WebApi.Features.Projects.GetProjectsList;

/// <summary>
/// Validator for GetProjectsListRequest
/// </summary>
public class GetProjectsListRequestValidator : AbstractValidator<GetProjectsListRequest>
{
    public GetProjectsListRequestValidator()
    {
        RuleFor(x => x.Page)
            .GreaterThan(0).WithMessage("Page number must be greater than 0");

        RuleFor(x => x.Size)
            .GreaterThan(0).WithMessage("Page size must be greater than 0")
            .LessThanOrEqualTo(100).WithMessage("Page size must not exceed 100");
    }
}
