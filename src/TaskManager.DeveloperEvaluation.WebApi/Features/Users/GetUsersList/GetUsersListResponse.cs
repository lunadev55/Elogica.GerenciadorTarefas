namespace TaskManager.DeveloperEvaluation.WebApi.Features.Users.GetUsersList;

/// <summary>
/// Response DTO for the users list
/// </summary>
public class GetUsersListResponse
{
    /// <summary>
    /// Gets or sets the list of users
    /// </summary>
    public IList<UserDto> Data { get; set; }

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

/// <summary>
/// DTO for user information in lists
/// </summary>
public class UserDto
{
    public Guid Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Status { get; set; }
    public string Role { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}

