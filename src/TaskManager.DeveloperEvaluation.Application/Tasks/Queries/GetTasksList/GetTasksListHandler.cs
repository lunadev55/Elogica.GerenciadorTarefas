using AutoMapper;
using MediatR;
using TaskManager.DeveloperEvaluation.Domain.Repositories;

namespace TaskManager.DeveloperEvaluation.Application.Tasks.Queries.GetTasksList
{
    public class GetTasksListHandler : IRequestHandler<GetTasksListQuery, GetTasksListResult>
    {
        private readonly ITaskRepository _repository;
        private readonly IMapper _mapper;

        public GetTasksListHandler(ITaskRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<GetTasksListResult> Handle(GetTasksListQuery request, CancellationToken cancellationToken)
        {
            var tasks = await _repository.ListAsync(request.Page, request.Size, request.OrderBy, cancellationToken);

            var items = _mapper.Map<IEnumerable<TaskListItem>>(tasks);
            var totalItems = tasks.Count();
            var totalPages = (int)System.Math.Ceiling((double)totalItems / request.Size);

            return new GetTasksListResult
            {
                Data = items,
                CurrentPage = request.Page,
                TotalPages = totalPages,
                TotalItems = totalItems
            };
        }
    }
}
