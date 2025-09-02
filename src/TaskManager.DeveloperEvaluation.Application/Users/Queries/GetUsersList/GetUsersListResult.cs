namespace TaskManager.DeveloperEvaluation.Application.Users.Queries.GetUsersList;

/// <summary>
/// Result containing paginated list of users
/// </summary>
public class GetUsersListResult
{
    /// <summary>
    /// Gets or sets the list of users
    /// </summary>
    public IEnumerable<UserListItem> Data { get; set; }

    /// <summary>
    /// Gets or sets the current page number
    /// </summary>
    public int CurrentPage { get; set; }

    /// <summary>
    /// Gets or sets the total number of pages
    /// </summary>
    public int TotalPages { get; set; }

    /// <summary>
    /// Gets or sets the total count of items
    /// </summary>
    public int TotalItems { get; set; }
}
