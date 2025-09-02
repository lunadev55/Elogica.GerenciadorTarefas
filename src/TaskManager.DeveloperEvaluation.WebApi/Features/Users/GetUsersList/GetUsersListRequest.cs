namespace TaskManager.DeveloperEvaluation.WebApi.Features.Users.GetUsersList
{
    /// <summary>
    /// Request DTO for retrieving a paginated list of users
    /// </summary>
    public class GetUsersListRequest
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
}
