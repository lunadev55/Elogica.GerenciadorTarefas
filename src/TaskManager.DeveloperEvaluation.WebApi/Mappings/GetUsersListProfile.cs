using AutoMapper;
using TaskManager.DeveloperEvaluation.Application.Users.Queries.GetUsersList;
using TaskManager.DeveloperEvaluation.WebApi.Features.Users.GetUsersList;

namespace TaskManager.DeveloperEvaluation.WebApi.Features.Users;

public class GetUsersListProfile : Profile
{
    public GetUsersListProfile()
    {
        CreateMap<GetUsersListResult, GetUsersListResponse>()
            .ForMember(dest => dest.Data, opt => opt.MapFrom(src => src.Data))
            .ForMember(dest => dest.CurrentPage, opt => opt.MapFrom(src => src.CurrentPage))
            .ForMember(dest => dest.TotalPages, opt => opt.MapFrom(src => src.TotalPages))
            .ForMember(dest => dest.TotalItems, opt => opt.MapFrom(src => src.TotalItems));

        CreateMap<UserListItem, UserDto>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role.ToString()));
    }
}
