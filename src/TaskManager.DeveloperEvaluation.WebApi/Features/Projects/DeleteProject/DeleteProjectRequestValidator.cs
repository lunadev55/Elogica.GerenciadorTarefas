using FluentValidation;

namespace TaskManager.DeveloperEvaluation.WebApi.Features.Projects.DeleteProject;

public class DeleteProjectRequestValidator : AbstractValidator<DeleteProjectRequest>
{
    public DeleteProjectRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Project ID is required");
    }
}