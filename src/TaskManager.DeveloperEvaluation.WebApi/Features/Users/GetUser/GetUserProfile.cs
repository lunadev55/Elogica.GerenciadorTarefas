using AutoMapper;

namespace TaskManager.DeveloperEvaluation.WebApi.Features.Users.GetUser;

/// <summary>
/// Profile for mapping GetUser feature requests to commands
/// </summary>
public class GetUserProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for GetUser feature
    /// </summary>
    public GetUserProfile()
    {     
        CreateMap<Guid, Application.Users.Queries.GetUserById.GetUserQuery>()
            .ConstructUsing(id => new Application.Users.Queries.GetUserById.GetUserQuery(id));
    }
}
