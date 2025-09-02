using FluentValidation;

namespace TaskManager.DeveloperEvaluation.WebApi.Features.Projects.CreateProjectFeature
{
    public class CreateProjectRequestValidator : AbstractValidator<CreateProjectRequest>
    {
        /// <summary>
        /// Initializes a new instance of the CreateUserRequestValidator with defined validation rules.
        /// </summary>
        /// <remarks>
        /// Validation rules include:        
        /// - Username: Required, length between 3 and 50 characters      
        /// </remarks>
        public CreateProjectRequestValidator()
        {           
            RuleFor(project => project.Name)
                .NotEmpty();
        }
    }
}
