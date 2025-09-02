using AutoMapper;
using TaskManager.DeveloperEvaluation.Application.Users.Queries.GetUserById;
using TaskManager.DeveloperEvaluation.WebApi.Features.Users.GetUser;

namespace TaskManager.DeveloperEvaluation.WebApi.Mappings
{
    public class GetUserProfile : Profile
    {
        public GetUserProfile()
        {            
            CreateMap<GetUserResult, GetUserResponse>()                
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))                
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))                
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role));
        }
    }
}
