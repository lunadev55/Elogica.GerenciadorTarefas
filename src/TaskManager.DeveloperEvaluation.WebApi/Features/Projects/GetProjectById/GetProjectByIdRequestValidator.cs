using FluentValidation;

namespace TaskManager.DeveloperEvaluation.WebApi.Features.Projects.GetProjectById;

/// <summary>
/// Validator for GetProjectByIdRequest.
/// </summary>
public class GetProjectByIdRequestValidator : AbstractValidator<GetProjectByIdRequest>
{
    public GetProjectByIdRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Project ID is required.");
    }
}
