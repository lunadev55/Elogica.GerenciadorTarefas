using AutoMapper;
using TaskManager.DeveloperEvaluation.Application.Projects.Queries.GetProjectsList;
using TaskManager.DeveloperEvaluation.Domain.Entities;
using TaskManager.DeveloperEvaluation.WebApi.Features.Projects.GetProjectsList;

namespace TaskManager.DeveloperEvaluation.WebApi.Mappings;

/// <summary>
/// Profile for mapping GetProjectsList feature results to responses
/// </summary>
public class GetProjectsListRequestProfile : Profile
{
    public GetProjectsListRequestProfile()
    {       
        CreateMap<GetProjectsListRequest, GetProjectsListQuery>()
            .ForMember(dest => dest.OrderBy, opt => opt.Ignore());
  
        CreateMap<Project, ProjectListItem>()
            .ForMember(dest => dest.Status,
                opt => opt.MapFrom(src => src.Status.ToString()));
   
        CreateMap<ProjectListItem, ProjectDto>()
            .ForMember(dest => dest.Status,
                opt => opt.MapFrom(src => src.Status.ToString()));    

        CreateMap<GetProjectsListResult, GetProjectsListResponse>()
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Data))
            .ForMember(dest => dest.TotalCount, opt => opt.MapFrom(src => src.TotalItems))
            .ForMember(dest => dest.PageSize,
                opt => opt.MapFrom(src =>
                    src.TotalItems == 0 ? 0 : (int)Math.Ceiling((double)src.TotalItems / src.TotalPages)))
            .ForMember(dest => dest.HasPrevious, opt => opt.MapFrom(src => src.CurrentPage > 1))
            .ForMember(dest => dest.HasNext, opt => opt.MapFrom(src => src.CurrentPage < src.TotalPages));
    }
}

