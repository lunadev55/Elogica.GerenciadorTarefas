using AutoMapper;
using MediatR;
using TaskManager.DeveloperEvaluation.Domain.Repositories;

namespace TaskManager.DeveloperEvaluation.Application.Users.Queries.GetUsersList
{
    /// <summary>
    /// Handler for retrieving paginated list of users
    /// </summary>
    public class GetUsersListHandler : IRequestHandler<GetUsersListQuery, GetUsersListResult>
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;

        public GetUsersListHandler(IUserRepository userRepository, IMapper mapper)
        {
            _repository = userRepository;
            _mapper = mapper;
        }

        public async Task<GetUsersListResult> Handle(GetUsersListQuery request, CancellationToken cancellationToken)
        {            
            var orderBy = "CreatedAt";

            var users = await _repository.ListAsync(request.Page, request.Size, orderBy, cancellationToken);
            var items = _mapper.Map<IEnumerable<UserListItem>>(users);

            var totalItems = users.Count;
            var totalPages = (int)Math.Ceiling((double)totalItems / request.Size);

            return new GetUsersListResult
            {
                Data = items,
                CurrentPage = request.Page,
                TotalPages = totalPages,
                TotalItems = totalItems
            };
        }
    }
}
