using AutoMapper;
using TaskManager.DeveloperEvaluation.Application.Projects.UpdateProject;

namespace TaskManager.DeveloperEvaluation.WebApi.Features.Projects.UpdateProject;

/// <summary>
/// AutoMapper profile for UpdateProject feature mappings.
/// </summary>
public class UpdateProjectProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateProjectProfile"/> class.
    /// </summary>
    public UpdateProjectProfile()
    {        
        CreateMap<UpdateProjectRequest, UpdateProjectCommand>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());
                
        CreateMap<UpdateProjectResult, UpdateProjectResponse>()
            .ForMember(dest => dest.Message, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());
    }
}