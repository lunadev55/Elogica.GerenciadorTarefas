using AutoMapper;
using TaskManager.DeveloperEvaluation.Domain.Entities;

namespace TaskManager.DeveloperEvaluation.Application.Users.Queries.GetUserById;

/// <summary>
/// Profile for mapping between User entity and GetUserResponse
/// </summary>
public class GetUserProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for GetUser operation
    /// </summary>
    public GetUserProfile()
    {
        CreateMap<User, GetUserResult>();
    }
}
