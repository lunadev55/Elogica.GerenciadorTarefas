using AutoMapper;
using TaskManager.DeveloperEvaluation.Application.Users.Queries.GetUsersList;

namespace TaskManager.DeveloperEvaluation.WebApi.Features.Users.GetUsersList
{
    /// <summary>
    /// Profile for mapping GetUsersList feature requests to queries
    /// </summary>
    public class GetUsersListRequestProfile : Profile
    {
        /// <summary>
        /// Initializes the mappings for GetUsersList feature
        /// </summary>
        public GetUsersListRequestProfile()
        {
            CreateMap<GetUsersListRequest, GetUsersListQuery>();
        }
    }
}
