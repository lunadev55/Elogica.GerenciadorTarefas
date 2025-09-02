using Microsoft.EntityFrameworkCore;
using TaskManager.DeveloperEvaluation.Common.Extensions;
using TaskManager.DeveloperEvaluation.Domain.Entities;
using TaskManager.DeveloperEvaluation.Domain.Enums;
using TaskManager.DeveloperEvaluation.Domain.Repositories;
using Task = TaskManager.DeveloperEvaluation.Domain.Entities.Task;
using TaskStatus = TaskManager.DeveloperEvaluation.Domain.Enums.TaskStatus;

namespace TaskManager.DeveloperEvaluation.ORM.Repositories;


/// <summary>
/// EF Core implementation of <see cref="ITaskRepository"/> using <see cref="DefaultContext"/>.
/// Provides data access for <see cref="Task"/> entities with comprehensive filtering
/// and bulk operations support.
/// </summary>
public class TaskRepository : ITaskRepository
{
    private readonly DefaultContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="TaskRepository"/> class.
    /// </summary>
    /// <param name="context">The EF Core <see cref="DefaultContext"/> to use.</param>
    public TaskRepository(DefaultContext context) => _context = context;

    /// <summary>
    /// Adds a new <see cref="Task"/> to the EF Core change‐tracker.
    /// Actual database insertion occurs when <see cref="SaveChangesAsync"/> is called.
    /// </summary>
    /// <param name="task">The <see cref="Task"/> entity to add.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> for async operation.</param>
    public async System.Threading.Tasks.Task AddAsync(TaskManager.DeveloperEvaluation.Domain.Entities.Task task, CancellationToken cancellationToken = default)
    {
        _context.Tasks.Add(task);
        await System.Threading.Tasks.Task.CompletedTask;
    }

    /// <summary>
    /// Marks an existing <see cref="Task"/> as modified so that its properties
    /// (e.g., <c>Title</c>, <c>Status</c>, <c>Priority</c>, <c>DueDate</c>) will be updated.
    /// </summary>
    /// <param name="task">The <see cref="Task"/> entity with updated properties.</param>
    public void Update(TaskManager.DeveloperEvaluation.Domain.Entities.Task task)
    {
        _context.Tasks.Update(task);
    }

    /// <summary>
    /// Retrieves a <see cref="Task"/> by its primary key.
    /// </summary>
    /// <param name="id">The <see cref="Task"/> identifier (GUID).</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> for async operation.</param>
    /// <returns>
    /// The matching <see cref="Task"/>, or <c>null</c> if no matching task exists.
    /// </returns>
    public async System.Threading.Tasks.Task<TaskManager.DeveloperEvaluation.Domain.Entities.Task> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Tasks
            .FirstOrDefaultAsync(t => t.Id == id, cancellationToken);
    }

    /// <summary>
    /// Retrieves all tasks for a specific project.
    /// </summary>
    /// <param name="projectId">The unique ID of the project whose tasks to fetch.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> for async operation.</param>
    /// <returns>A read‐only list of <see cref="Task"/> entities for the specified project.</returns>
    public async System.Threading.Tasks.Task<IReadOnlyList<TaskManager.DeveloperEvaluation.Domain.Entities.Task>> GetByProjectIdAsync(Guid projectId, CancellationToken cancellationToken = default)
    {
        return await _context.Tasks
            .Where(t => t.ProjectId == projectId)
            .OrderBy(t => t.DueDate)
            .ToListAsync(cancellationToken);
    }

    /// <summary>
    /// Retrieves all tasks assigned to a specific user.
    /// </summary>
    /// <param name="userId">The unique ID of the user whose tasks to fetch.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> for async operation.</param>
    /// <returns>A read‐only list of <see cref="Task"/> entities assigned to the specified user.</returns>
    public async System.Threading.Tasks.Task<IReadOnlyList<TaskManager.DeveloperEvaluation.Domain.Entities.Task>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await _context.Tasks
            .Where(t => t.UserId == userId)
            .OrderBy(t => t.DueDate)
            .ToListAsync(cancellationToken);
    }

    /// <summary>
    /// Returns a paginated list of <see cref="Task"/> entities, ordered by <c>CreatedAt</c>.
    /// </summary>
    /// <param name="page">The 1‐based page number.</param>
    /// <param name="size">The number of items per page.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> for async operation.</param>
    /// <returns>A read‐only list of <see cref="Task"/> entities for the requested page.</returns>
    public async System.Threading.Tasks.Task<IReadOnlyList<TaskManager.DeveloperEvaluation.Domain.Entities.Task>> ListAsync(int page, int size, string orderBy = null, CancellationToken cancellationToken = default)
    {
        IQueryable<Task> query = _context.Tasks;

        if (!string.IsNullOrWhiteSpace(orderBy))
        {
            var orders = orderBy.Split(',');
            foreach (var ord in orders)
            {
                var parts = ord.Trim().Split(' ');
                var prop = parts[0];
                var desc = parts.Length > 1 && parts[1].Equals("desc", StringComparison.OrdinalIgnoreCase);                
                query = QueryableExtensions
                    .OrderByProperty(query, prop, desc);
            }
        }
        else
        {
            query = query.OrderBy(p => p.Id);
        }

        return await query
            .Skip((page - 1) * size)
            .Take(size)
            .ToListAsync(cancellationToken);
    }

    /// <summary>
    /// Retrieves tasks filtered by status with pagination.
    /// </summary>
    /// <param name="status">The task status to filter by.</param>
    /// <param name="page">The 1‐based page number.</param>
    /// <param name="size">The number of items per page.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> for async operation.</param>
    /// <returns>A read‐only list of <see cref="Task"/> entities with the specified status.</returns>
    public async System.Threading.Tasks.Task<IReadOnlyList<TaskManager.DeveloperEvaluation.Domain.Entities.Task>> GetByStatusAsync(TaskStatus status, int page, int size, CancellationToken cancellationToken = default)
    {
        return await _context.Tasks
            .Where(t => t.Status == status)
            .OrderBy(t => t.DueDate)
            .Skip((page - 1) * size)
            .Take(size)
            .ToListAsync(cancellationToken);
    }

    /// <summary>
    /// Retrieves tasks filtered by priority with pagination.
    /// </summary>
    /// <param name="priority">The task priority to filter by.</param>
    /// <param name="page">The 1‐based page number.</param>
    /// <param name="size">The number of items per page.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> for async operation.</param>
    /// <returns>A read‐only list of <see cref="Task"/> entities with the specified priority.</returns>
    public async System.Threading.Tasks.Task<IReadOnlyList<TaskManager.DeveloperEvaluation.Domain.Entities.Task>> GetByPriorityAsync(TaskPriority priority, int page, int size, CancellationToken cancellationToken = default)
    {
        return await _context.Tasks
            .Where(t => t.Priority == priority)
            .OrderBy(t => t.DueDate)
            .Skip((page - 1) * size)
            .Take(size)
            .ToListAsync(cancellationToken);
    }

    /// <summary>
    /// Retrieves tasks that are due before or on the specified date.
    /// </summary>
    /// <param name="dueDate">The due date to filter by.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> for async operation.</param>
    /// <returns>A read‐only list of <see cref="Task"/> entities due on or before the specified date.</returns>
    public async System.Threading.Tasks.Task<IReadOnlyList<TaskManager.DeveloperEvaluation.Domain.Entities.Task>> GetTasksDueBeforeAsync(DateTime dueDate, CancellationToken cancellationToken = default)
    {
        return await _context.Tasks
            .Where(t => t.DueDate <= dueDate)
            .OrderBy(t => t.DueDate)
            .ToListAsync(cancellationToken);
    }

    /// <summary>
    /// Saves all pending changes in the <see cref="DefaultContext"/> to the database.
    /// </summary>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> for async operation.</param>
    public async System.Threading.Tasks.Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }

    /// <summary>
    /// Inserts a collection of new <see cref="Task"/> rows for the specified <see cref="Project"/> ID
    /// using raw SQL. Assumes each <see cref="Task"/> in <paramref name="tasks"/> has a valid GUID
    /// and pre‐computed properties.
    /// </summary>
    /// <param name="tasks">The list of <see cref="Task"/> entities to insert.</param>
    /// <param name="projectId">The parent <see cref="Project"/> identifier (foreign key).</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> for async operation.</param>
    public async System.Threading.Tasks.Task AddTasksAsync(IEnumerable<TaskManager.DeveloperEvaluation.Domain.Entities.Task> tasks, Guid projectId, CancellationToken cancellationToken = default)
    {
        const string insertSql = @"
                INSERT INTO ""Tasks"" (
                    ""Id"",
                    ""Title"",
                    ""Description"",
                    ""DueDate"",
                    ""Status"",
                    ""Priority"",
                    ""ProjectId"",
                    ""UserId"",
                    ""CreatedAt"",
                    ""UpdatedAt""
                ) VALUES (
                    {0},  -- Task.Id
                    {1},  -- Title
                    {2},  -- Description
                    {3},  -- DueDate
                    {4},  -- Status
                    {5},  -- Priority
                    {6},  -- ProjectId (foreign key)
                    {7},  -- UserId
                    {8},  -- CreatedAt
                    {9}   -- UpdatedAt
                );";

        foreach (var task in tasks)
        {
            await _context.Database.ExecuteSqlRawAsync(
                insertSql,
                new object[]
                {
                    task.Id,
                    task.Title,
                    task.Description,
                    task.DueDate,
                    (int)task.Status,
                    (int)task.Priority,
                    projectId,
                    task.UserId,
                    task.CreatedAt,
                    task.UpdatedAt
                },
                cancellationToken
            );
        }
    }

    /// <summary>
    /// Soft deletes a <see cref="Task"/> by removing it from the context.
    /// </summary>
    /// <param name="id">The unique ID of the task to delete.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> for async operation.</param>
    public async System.Threading.Tasks.Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var task = await GetByIdAsync(id, cancellationToken);
        if (task != null)
        {
            _context.Tasks.Remove(task);
        }
    }

    /// <summary>
    /// Deletes all tasks associated with the specified project ID.
    /// This method uses a bulk delete operation for better performance.
    /// </summary>
    /// <param name="projectId">The unique identifier of the project whose tasks should be deleted.</param>
    /// <param name="cancellationToken">A token to observe while awaiting the asynchronous operation.</param>
    /// <returns>A task that represents the asynchronous delete operation.</returns>
    public async System.Threading.Tasks.Task DeleteAllTasksByProjectIdAsync(Guid projectId, CancellationToken cancellationToken = default)
    {        
        await _context.Tasks
            .Where(t => t.ProjectId == projectId)
            .ExecuteDeleteAsync(cancellationToken);
    }
}