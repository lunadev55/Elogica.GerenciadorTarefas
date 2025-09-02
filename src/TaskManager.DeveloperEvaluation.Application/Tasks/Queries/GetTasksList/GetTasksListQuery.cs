using MediatR;

namespace TaskManager.DeveloperEvaluation.Application.Tasks.Queries.GetTasksList;

public class GetTasksListQuery : IRequest<GetTasksListResult>
{
    public int Page { get; set; }
    public int Size { get; set; }
    public string OrderBy { get; set; }
}
