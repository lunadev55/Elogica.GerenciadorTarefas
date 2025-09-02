using TaskManager.DeveloperEvaluation.Domain.Entities;
using TaskManager.DeveloperEvaluation.Domain.Enums;

namespace TaskManager.DeveloperEvaluation.Domain.Repositories
{
    /// <summary>
    /// Defines the contract for persisting and retrieving <see cref="Project"/> aggregates,
    /// as well as managing their associated <see cref="Task"/> entities.
    /// </summary>
    public interface IProjectRepository
    {
        /// <summary>
        /// Adds a new <see cref="Project"/> to the underlying data store.
        /// </summary>
        /// <param name="project">The project aggregate to add.</param>
        /// <param name="cancellationToken">
        /// A token to monitor for cancellation requests.
        /// </param>
        System.Threading.Tasks.Task AddAsync(Project project, CancellationToken cancellationToken = default);

        /// <summary>
        /// Marks an existing <see cref="Project"/> as modified so that changes
        /// will be persisted on the next <see cref="SaveChangesAsync"/> call.
        /// </summary>
        /// <param name="project">The project aggregate with updated state.</param>
        void Update(Project project);

        /// <summary>
        /// Retrieves a <see cref="Project"/> by its unique identifier, including any associated tasks.
        /// </summary>
        /// <param name="id">The unique ID of the project to fetch.</param>
        /// <param name="cancellationToken">
        /// A token to monitor for cancellation requests.
        /// </param>
        /// <returns>
        /// The <see cref="Project"/> with the specified ID, or <c>null</c> if not found.
        /// </returns>
        System.Threading.Tasks.Task<Project> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves all projects for a specific user.
        /// </summary>
        /// <param name="userId">The unique ID of the user whose projects to fetch.</param>
        /// <param name="cancellationToken">
        /// A token to monitor for cancellation requests.
        /// </param>
        /// <returns>
        /// A read-only list of <see cref="Project"/> instances for the specified user.
        /// </returns>
        System.Threading.Tasks.Task<IReadOnlyList<Project>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves a paginated list of <see cref="Project"/> aggregates.
        /// </summary>
        /// <param name="page">The page number (1-based) to fetch.</param>
        /// <param name="size">The number of projects per page.</param>
        /// <param name="cancellationToken">
        /// A token to monitor for cancellation requests.
        /// </param>
        /// <returns>
        /// A read-only list of <see cref="Project"/> instances for the specified page.
        /// </returns>
        System.Threading.Tasks.Task<IReadOnlyList<Project>> ListAsync(int page, int size, string orderBy, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves projects filtered by status with pagination.
        /// </summary>
        /// <param name="status">The project status to filter by.</param>
        /// <param name="page">The page number (1-based) to fetch.</param>
        /// <param name="size">The number of projects per page.</param>
        /// <param name="cancellationToken">
        /// A token to monitor for cancellation requests.
        /// </param>
        /// <returns>
        /// A read-only list of <see cref="Project"/> instances with the specified status.
        /// </returns>
        System.Threading.Tasks.Task<IReadOnlyList<Project>> GetByStatusAsync(ProjectStatus status, int page, int size, CancellationToken cancellationToken = default);

        /// <summary>
        /// Persists all pending changes (inserts, updates, deletes) to the data store.
        /// </summary>
        /// <param name="cancellationToken">
        /// A token to monitor for cancellation requests.
        /// </param>
        System.Threading.Tasks.Task SaveChangesAsync(CancellationToken cancellationToken = default);

        ///// <summary>
        ///// Deletes all <see cref="Task"/> rows associated with a given <see cref="Project"/>.
        ///// Useful when replacing or clearing tasks for that project.
        ///// </summary>
        ///// <param name="projectId">The ID of the project whose tasks should be removed.</param>
        ///// <param name="cancellationToken">
        ///// A token to monitor for cancellation requests.
        ///// </param>
        //System.Threading.Tasks.Task DeleteAllTasksByProjectIdAsync(Guid projectId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Soft deletes a <see cref="Project"/> by marking it as deleted.
        /// </summary>
        /// <param name="id">The unique ID of the project to delete.</param>
        /// <param name="cancellationToken">
        /// A token to monitor for cancellation requests.
        /// </param>
        System.Threading.Tasks.Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    }    
}