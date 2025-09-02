using MediatR;

namespace TaskManager.DeveloperEvaluation.Application.Projects.Queries.GetProjectById;

public class GetProjectByIdQuery : IRequest<GetProjectByIdResult>
{
    public Guid Id { get; set; }
}
