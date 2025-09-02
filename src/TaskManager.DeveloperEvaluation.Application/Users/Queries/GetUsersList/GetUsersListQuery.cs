using MediatR;

namespace TaskManager.DeveloperEvaluation.Application.Users.Queries.GetUsersList;

/// <summary>
/// Query to retrieve a paginated list of users
/// </summary>
public class GetUsersListQuery : IRequest<GetUsersListResult>
{
    /// <summary>
    /// Gets or sets the page number (1-based)
    /// </summary>
    public int Page { get; set; }

    /// <summary>
    /// Gets or sets the page size
    /// </summary>
    public int Size { get; set; }
}
