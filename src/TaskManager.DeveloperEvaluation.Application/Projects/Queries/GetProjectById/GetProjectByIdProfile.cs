using AutoMapper;
using TaskManager.DeveloperEvaluation.Domain.Entities;

namespace TaskManager.DeveloperEvaluation.Application.Projects.Queries.GetProjectById
{
    public class GetProjectByIdProfile : Profile
    {
        public GetProjectByIdProfile()
        {
            CreateMap<Project, GetProjectByIdResult>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));
        }
    }
}
