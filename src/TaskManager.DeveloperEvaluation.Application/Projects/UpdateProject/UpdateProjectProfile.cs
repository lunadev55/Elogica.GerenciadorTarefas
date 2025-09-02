using AutoMapper;
using TaskManager.DeveloperEvaluation.Domain.Entities;

namespace TaskManager.DeveloperEvaluation.Application.Projects.UpdateProject;

/// <summary>
/// AutoMapper profile for mapping between <see cref="UpdateProjectCommand"/> 
/// DTOs and domain <see cref="Project"/> entities.
/// </summary>
public class UpdateProjectProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateProjectProfile"/> class,
    /// configuring mappings for updating existing projects.
    /// </summary>
    public UpdateProjectProfile()
    {
        CreateMap<UpdateProjectCommand, Project>()
            .ForMember(
                dest => dest.Id,
                opt => opt.MapFrom(src => src.Id))
            .ForMember(
                dest => dest.CreatedAt,
                opt => opt.Ignore())
            .ForMember(
                dest => dest.UpdatedAt,
                opt => opt.MapFrom(src => DateTime.UtcNow));
    }
}