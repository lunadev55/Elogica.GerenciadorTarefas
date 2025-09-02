using AutoMapper;
using TaskManager.DeveloperEvaluation.Application.Projects.CreateProject;

namespace TaskManager.DeveloperEvaluation.WebApi.Features.Projects.CreateProjectFeature;

/// <summary>
/// AutoMapper profile for CreateProject feature mappings.
/// </summary>
public class CreateProjectProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateProjectProfile"/> class.
    /// </summary>
    public CreateProjectProfile()
    {        
        CreateMap<CreateProjectRequest, CreateProjectCommand>();
        
        CreateMap<CreateProjectResult, CreateProjectResponse>()
            .ForMember(dest => dest.Message, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore());
    }
}