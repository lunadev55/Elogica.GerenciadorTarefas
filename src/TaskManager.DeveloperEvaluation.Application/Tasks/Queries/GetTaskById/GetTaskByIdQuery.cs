using MediatR;

namespace TaskManager.DeveloperEvaluation.Application.Tasks.Queries.GetTaskById;

public class GetTaskByIdQuery : IRequest<GetTaskByIdResult>
{
    public Guid Id { get; set; }
}
