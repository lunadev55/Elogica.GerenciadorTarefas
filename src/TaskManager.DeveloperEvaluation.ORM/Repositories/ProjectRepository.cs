using Microsoft.EntityFrameworkCore;
using TaskManager.DeveloperEvaluation.Common.Extensions;
using TaskManager.DeveloperEvaluation.Domain.Entities;
using TaskManager.DeveloperEvaluation.Domain.Enums;
using TaskManager.DeveloperEvaluation.Domain.Repositories;

namespace TaskManager.DeveloperEvaluation.ORM.Repositories;

/// <summary>
/// EF Core implementation of <see cref="IProjectRepository"/> using <see cref="DefaultContext"/>.
/// Provides data access for <see cref="Project"/> aggregates, including header updates
/// and raw‐SQL operations for child <see cref="Task"/> collections.
/// </summary>
public class ProjectRepository : IProjectRepository
{
    private readonly DefaultContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="ProjectRepository"/> class.
    /// </summary>
    /// <param name="context">The EF Core <see cref="DefaultContext"/> to use.</param>
    public ProjectRepository(DefaultContext context) => _context = context;

    /// <summary>
    /// Adds a new <see cref="Project"/> to the EF Core change‐tracker.
    /// Actual database insertion occurs when <see cref="SaveChangesAsync"/> is called.
    /// </summary>
    /// <param name="project">The <see cref="Project"/> entity to add.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> for async operation.</param>
    public async System.Threading.Tasks.Task AddAsync(Project project, CancellationToken cancellationToken = default)
    {
        _context.Projects.Add(project);
        await System.Threading.Tasks.Task.CompletedTask;
    }

    /// <summary>
    /// Marks an existing <see cref="Project"/> as modified so that its header properties
    /// (e.g., <c>Name</c>, <c>Description</c>, <c>Status</c>, <c>EndDate</c>) will be updated.
    /// Child <see cref="Task"/> rows are handled separately via raw‐SQL methods.
    /// </summary>
    /// <param name="project">The <see cref="Project"/> entity with updated properties.</param>
    public void Update(Project project)
    {
        _context.Projects.Update(project);
    }

    /// <summary>
    /// Retrieves a <see cref="Project"/> by its primary key, including its child tasks.
    /// </summary>
    /// <param name="id">The <see cref="Project"/> identifier (GUID).</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> for async operation.</param>
    /// <returns>
    /// The matching <see cref="Project"/> (with its tasks collection), or <c>null</c>
    /// if no matching project exists.
    /// </returns>
    public async System.Threading.Tasks.Task<Project> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Projects
            .Include(p => p.Tasks)
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    /// <summary>
    /// Retrieves all projects for a specific user.
    /// </summary>
    /// <param name="userId">The unique ID of the user whose projects to fetch.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> for async operation.</param>
    /// <returns>A read‐only list of <see cref="Project"/> entities for the specified user.</returns>
    public async System.Threading.Tasks.Task<IReadOnlyList<Project>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await _context.Projects
            .Where(p => p.UserId == userId)
            .OrderBy(p => p.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    /// <summary>
    /// Returns a paginated list of <see cref="Project"/> entities, ordered by <c>CreatedAt</c>.
    /// </summary>
    /// <param name="page">The 1‐based page number.</param>
    /// <param name="size">The number of items per page.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> for async operation.</param>
    /// <returns>A read‐only list of <see cref="Project"/> entities for the requested page.</returns>
    public async System.Threading.Tasks.Task<IReadOnlyList<Project>> ListAsync(int page, int size, string orderBy = null, CancellationToken cancellationToken = default)
    {
        //return await _context.Projects
        //    .OrderBy(p => p.CreatedAt)
        //    .Skip((page - 1) * size)
        //    .Take(size)
        //    .ToListAsync(cancellationToken);

        IQueryable<Project> query = _context.Projects;

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
    /// Retrieves projects filtered by status with pagination.
    /// </summary>
    /// <param name="status">The project status to filter by.</param>
    /// <param name="page">The 1‐based page number.</param>
    /// <param name="size">The number of items per page.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> for async operation.</param>
    /// <returns>A read‐only list of <see cref="Project"/> entities with the specified status.</returns>
    public async System.Threading.Tasks.Task<IReadOnlyList<Project>> GetByStatusAsync(ProjectStatus status, int page, int size, CancellationToken cancellationToken = default)
    {
        return await _context.Projects
            .Where(p => p.Status == status)
            .OrderBy(p => p.CreatedAt)
            .Skip((page - 1) * size)
            .Take(size)
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
    /// Soft deletes a <see cref="Project"/> by removing it from the context.
    /// </summary>
    /// <param name="id">The unique ID of the project to delete.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> for async operation.</param>
    public async System.Threading.Tasks.Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var project = await GetByIdAsync(id, cancellationToken);
        if (project != null)
        {
            _context.Projects.Remove(project);
        }
    }
}
