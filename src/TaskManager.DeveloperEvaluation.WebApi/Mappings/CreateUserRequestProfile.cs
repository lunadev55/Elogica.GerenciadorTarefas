using AutoMapper;
using TaskManager.DeveloperEvaluation.Application.Users.CreateUser;
using TaskManager.DeveloperEvaluation.WebApi.Features.Users.CreateUser;

namespace TaskManager.DeveloperEvaluation.WebApi.Mappings;

public class CreateUserRequestProfile : Profile
{
    public CreateUserRequestProfile()
    {
        CreateMap<CreateUserRequest, CreateUserCommand>();
    }
}