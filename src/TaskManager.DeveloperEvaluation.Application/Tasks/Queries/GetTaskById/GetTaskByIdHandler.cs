using AutoMapper;
using MediatR;
using TaskManager.DeveloperEvaluation.Domain.Repositories;

namespace TaskManager.DeveloperEvaluation.Application.Tasks.Queries.GetTaskById;

public class GetTaskByIdHandler : IRequestHandler<GetTaskByIdQuery, GetTaskByIdResult>
{
    private readonly ITaskRepository _repository;
    private readonly IMapper _mapper;

    public GetTaskByIdHandler(ITaskRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<GetTaskByIdResult> Handle(GetTaskByIdQuery request, CancellationToken cancellationToken)
    {
        var task = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (task == null)
            throw new KeyNotFoundException($"Task '{request.Id}' not found.");

        return _mapper.Map<GetTaskByIdResult>(task);
    }
}
