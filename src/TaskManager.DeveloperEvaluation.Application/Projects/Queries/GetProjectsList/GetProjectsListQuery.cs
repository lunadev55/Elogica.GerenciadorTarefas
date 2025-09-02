using MediatR;

namespace TaskManager.DeveloperEvaluation.Application.Projects.Queries.GetProjectsList;

public class GetProjectsListQuery : IRequest<GetProjectsListResult>
{
    public int Page { get; set; }
    public int Size { get; set; }
    public string OrderBy { get; set; }
}
