using AutoMapper;
using TaskManager.DeveloperEvaluation.Application.Users.Queries.GetUserById;
using TaskManager.DeveloperEvaluation.WebApi.Features.Users.GetUser;

namespace TaskManager.DeveloperEvaluation.WebApi.Mappings;

public class GetUserByIdRequestProfile : Profile
{
    public GetUserByIdRequestProfile()
    {
        CreateMap<GetUserRequest, GetUserQuery>();
    }
}
