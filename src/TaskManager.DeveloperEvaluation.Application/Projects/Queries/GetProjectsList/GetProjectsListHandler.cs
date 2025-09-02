using AutoMapper;
using MediatR;
using TaskManager.DeveloperEvaluation.Domain.Repositories;

namespace TaskManager.DeveloperEvaluation.Application.Projects.Queries.GetProjectsList
{
    public class GetProjectsListHandler : IRequestHandler<GetProjectsListQuery, GetProjectsListResult>
    {
        private readonly IProjectRepository _repository;
        private readonly IMapper _mapper;

        public GetProjectsListHandler(IProjectRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<GetProjectsListResult> Handle(GetProjectsListQuery request, CancellationToken cancellationToken)
        {
            var projects = await _repository.ListAsync(request.Page, request.Size, request.OrderBy, cancellationToken);
            var items = _mapper.Map<IEnumerable<ProjectListItem>>(projects);

            var totalItems = projects.Count();
            var totalPages = (int)System.Math.Ceiling((double)totalItems / request.Size);

            return new GetProjectsListResult
            {
                Data = items,
                CurrentPage = request.Page,
                TotalPages = totalPages,
                TotalItems = totalItems
            };
        }
    }
}
