using AutoMapper;
using TaskManager.DeveloperEvaluation.Domain.Entities;

namespace TaskManager.DeveloperEvaluation.Application.Projects.CreateProject
{
    /// <summary>
    /// AutoMapper profile for mapping between CreateProject DTOs and domain entities.
    /// </summary>
    public class CreateProjectProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateProjectProfile"/> class,
        /// configuring mappings from <see cref="CreateProjectCommand"/> to <see cref="Project"/>.
        /// </summary>
        public CreateProjectProfile()
        {
            CreateMap<CreateProjectCommand, Project>()
                .ConstructUsing(cmd => new Project(
                    Guid.NewGuid(),
                    cmd.Name,
                    cmd.Description,
                    cmd.StartDate,
                    cmd.EndDate,
                    cmd.Status,
                    cmd.UserId,
                    DateTime.UtcNow));
        }
    }
}