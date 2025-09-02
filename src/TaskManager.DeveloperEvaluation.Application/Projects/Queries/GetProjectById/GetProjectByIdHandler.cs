using AutoMapper;
using MediatR;
using TaskManager.DeveloperEvaluation.Domain.Repositories;

namespace TaskManager.DeveloperEvaluation.Application.Projects.Queries.GetProjectById
{
    public class GetProjectByIdHandler : IRequestHandler<GetProjectByIdQuery, GetProjectByIdResult>
    {
        private readonly IProjectRepository _repository;
        private readonly IMapper _mapper;

        public GetProjectByIdHandler(IProjectRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<GetProjectByIdResult> Handle(GetProjectByIdQuery request, CancellationToken cancellationToken)
        {
            var project = await _repository.GetByIdAsync(request.Id, cancellationToken);
            if (project == null)
                throw new KeyNotFoundException($"Project '{request.Id}' not found.");

            return _mapper.Map<GetProjectByIdResult>(project);
        }
    }
}
