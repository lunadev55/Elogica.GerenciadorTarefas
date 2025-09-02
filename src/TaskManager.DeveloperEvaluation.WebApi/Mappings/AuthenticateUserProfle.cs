using AutoMapper;
using TaskManager.DeveloperEvaluation.Application.Auth.AuthenticateUser;
using TaskManager.DeveloperEvaluation.WebApi.Features.Auth.AuthenticateUserFeature;

namespace TaskManager.DeveloperEvaluation.WebApi.Mappings;

public class AuthenticateUserProfile : Profile
{
    public AuthenticateUserProfile()
    {        
        CreateMap<AuthenticateUserResult, AuthenticateUserResponse>();
    }
}
