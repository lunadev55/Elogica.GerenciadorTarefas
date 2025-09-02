using TaskManager.DeveloperEvaluation.Domain.Enums;

namespace TaskManager.DeveloperEvaluation.Application.Users.Queries.GetUsersList;

/// <summary>
/// Represents a user item in the list response
/// </summary>
public class UserListItem
{
    /// <summary>
    /// Gets or sets the unique identifier of the user
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the username
    /// </summary>
    public string Username { get; set; }

    /// <summary>
    /// Gets or sets the email address
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// Gets or sets the phone number
    /// </summary>
    public string Phone { get; set; }

    /// <summary>
    /// Gets or sets the user status
    /// </summary>
    public UserStatus Status { get; set; }

    /// <summary>
    /// Gets or sets the user role
    /// </summary>
    public UserRole Role { get; set; }

    /// <summary>
    /// Gets or sets when the user was created
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets when the user was last updated
    /// </summary>
    public DateTime? UpdatedAt { get; set; }
}
