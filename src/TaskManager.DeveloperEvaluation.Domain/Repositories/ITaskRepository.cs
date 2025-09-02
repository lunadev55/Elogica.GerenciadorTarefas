using TaskManager.DeveloperEvaluation.Domain.Entities;
using TaskManager.DeveloperEvaluation.Domain.Enums;
using Task = TaskManager.DeveloperEvaluation.Domain.Entities.Task;
using TaskStatus = TaskManager.DeveloperEvaluation.Domain.Enums.TaskStatus;

namespace TaskManager.DeveloperEvaluation.Domain.Repositories
{   

    /// <summary>
    /// Defines the contract for persisting and retrieving <see cref="Task"/> entities
    /// and managing their relationship with <see cref="Project"/> aggregates.
    /// </summary>
    public interface ITaskRepository
    {
        /// <summary>
        /// Adds a new <see cref="Task"/> to the underlying data store.
        /// </summary>
        /// <param name="task">The task entity to add.</param>
        /// <param name="cancellationToken">
        /// A token to monitor for cancellation requests.
        /// </param>
        System.Threading.Tasks.Task AddAsync(Task task, CancellationToken cancellationToken = default);

        /// <summary>
        /// Marks an existing <see cref="Task"/> as modified so that changes
        /// will be persisted on the next <see cref="SaveChangesAsync"/> call.
        /// </summary>
        /// <param name="task">The task entity with updated state.</param>
        void Update(Task task);

        /// <summary>
        /// Retrieves a <see cref="Task"/> by its unique identifier.
        /// </summary>
        /// <param name="id">The unique ID of the task to fetch.</param>
        /// <param name="cancellationToken">
        /// A token to monitor for cancellation requests.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/> with the specified ID, or <c>null</c> if not found.
        /// </returns>
        System.Threading.Tasks.Task<Task> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves all tasks for a specific project.
        /// </summary>
        /// <param name="projectId">The unique ID of the project whose tasks to fetch.</param>
        /// <param name="cancellationToken">
        /// A token to monitor for cancellation requests.
        /// </param>
        /// <returns>
        /// A read-only list of <see cref="Task"/> instances for the specified project.
        /// </returns>
        System.Threading.Tasks.Task<IReadOnlyList<Task>> GetByProjectIdAsync(Guid projectId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves all tasks assigned to a specific user.
        /// </summary>
        /// <param name="userId">The unique ID of the user whose tasks to fetch.</param>
        /// <param name="cancellationToken">
        /// A token to monitor for cancellation requests.
        /// </param>
        /// <returns>
        /// A read-only list of <see cref="Task"/> instances assigned to the specified user.
        /// </returns>
        System.Threading.Tasks.Task<IReadOnlyList<Task>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves a paginated list of <see cref="Task"/> entities.
        /// </summary>
        /// <param name="page">The page number (1-based) to fetch.</param>
        /// <param name="size">The number of tasks per page.</param>
        /// <param name="cancellationToken">
        /// A token to monitor for cancellation requests.
        /// </param>
        /// <returns>
        /// A read-only list of <see cref="Task"/> instances for the specified page.
        /// </returns>
        System.Threading.Tasks.Task<IReadOnlyList<Task>> ListAsync(int page, int size, string orderBy, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves tasks filtered by status with pagination.
        /// </summary>
        /// <param name="status">The task status to filter by.</param>
        /// <param name="page">The page number (1-based) to fetch.</param>
        /// <param name="size">The number of tasks per page.</param>
        /// <param name="cancellationToken">
        /// A token to monitor for cancellation requests.
        /// </param>
        /// <returns>
        /// A read-only list of <see cref="Task"/> instances with the specified status.
        /// </returns>
        System.Threading.Tasks.Task<IReadOnlyList<Task>> GetByStatusAsync(TaskStatus status, int page, int size, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves tasks filtered by priority with pagination.
        /// </summary>
        /// <param name="priority">The task priority to filter by.</param>
        /// <param name="page">The page number (1-based) to fetch.</param>
        /// <param name="size">The number of tasks per page.</param>
        /// <param name="cancellationToken">
        /// A token to monitor for cancellation requests.
        /// </param>
        /// <returns>
        /// A read-only list of <see cref="Task"/> instances with the specified priority.
        /// </returns>
        System.Threading.Tasks.Task<IReadOnlyList<Task>> GetByPriorityAsync(TaskPriority priority, int page, int size, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves tasks that are due before or on the specified date.
        /// </summary>
        /// <param name="dueDate">The due date to filter by.</param>
        /// <param name="cancellationToken">
        /// A token to monitor for cancellation requests.
        /// </param>
        /// <returns>
        /// A read-only list of <see cref="Task"/> instances due on or before the specified date.
        /// </returns>
        System.Threading.Tasks.Task<IReadOnlyList<Task>> GetTasksDueBeforeAsync(DateTime dueDate, CancellationToken cancellationToken = default);

        /// <summary>
        /// Persists all pending changes (inserts, updates, deletes) to the data store.
        /// </summary>
        /// <param name="cancellationToken">
        /// A token to monitor for cancellation requests.
        /// </param>
        System.Threading.Tasks.Task SaveChangesAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Inserts a batch of <see cref="Task"/> instances for a specific <see cref="Project"/>.
        /// </summary>
        /// <param name="tasks">The collection of tasks to add to the project.</param>
        /// <param name="projectId">
        /// The ID of the project under which these tasks should be grouped.
        /// </param>
        /// <param name="cancellationToken">
        /// A token to monitor for cancellation requests.
        /// </param>
        System.Threading.Tasks.Task AddTasksAsync(IEnumerable<Task> tasks, Guid projectId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Soft deletes a <see cref="Task"/> by marking it as deleted.
        /// </summary>
        /// <param name="id">The unique ID of the task to delete.</param>
        /// <param name="cancellationToken">
        /// A token to monitor for cancellation requests.
        /// </param>
        System.Threading.Tasks.Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes all tasks associated with the specified project ID.
        /// </summary>
        /// <param name="projectId">The unique identifier of the project whose tasks should be deleted.</param>
        /// <param name="cancellationToken">A token to observe while awaiting the asynchronous operation.</param>
        /// <returns>A task that represents the asynchronous delete operation.</returns>
        System.Threading.Tasks.Task DeleteAllTasksByProjectIdAsync(Guid projectId, CancellationToken cancellationToken = default);
    }
}