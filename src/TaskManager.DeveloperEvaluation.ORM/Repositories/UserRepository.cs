using Microsoft.EntityFrameworkCore;
using TaskManager.DeveloperEvaluation.Common.Extensions;
using TaskManager.DeveloperEvaluation.Domain.Entities;
using TaskManager.DeveloperEvaluation.Domain.Repositories;

namespace TaskManager.DeveloperEvaluation.ORM.Repositories;

/// <summary>
/// Implementation of IUserRepository using Entity Framework Core
/// </summary>
public class UserRepository : IUserRepository
{
    private readonly DefaultContext _context;

    /// <summary>
    /// Initializes a new instance of UserRepository
    /// </summary>
    /// <param name="context">The database context</param>
    public UserRepository(DefaultContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Creates a new user in the database
    /// </summary>
    /// <param name="user">The user to create</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created user</returns>
    public async Task<User> CreateAsync(User user, CancellationToken cancellationToken = default)
    {
        await _context.Users.AddAsync(user, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return user;
    }

    /// <summary>
    /// Retrieves a user by their unique identifier
    /// </summary>
    /// <param name="id">The unique identifier of the user</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The user if found, null otherwise</returns>
    public async Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Users.FirstOrDefaultAsync(o=> o.Id == id, cancellationToken);
    }

    /// <summary>
    /// Retrieves a user by their email address
    /// </summary>
    /// <param name="email">The email address to search for</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The user if found, null otherwise</returns>
    public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return await _context.Users
            .FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
    }

    /// <summary>
    /// Returns a paginated list of <see cref="Project"/> entities, ordered by <c>CreatedAt</c>.
    /// </summary>
    /// <param name="page">The 1‐based page number.</param>
    /// <param name="size">The number of items per page.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> for async operation.</param>
    /// <returns>A read‐only list of <see cref="Project"/> entities for the requested page.</returns>
    public async System.Threading.Tasks.Task<IReadOnlyList<User>> ListAsync(int page, int size, string orderBy = null, CancellationToken cancellationToken = default)
    {     
        IQueryable<User> query = _context.Users;

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
    /// Deletes a user from the database
    /// </summary>
    /// <param name="id">The unique identifier of the user to delete</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if the user was deleted, false if not found</returns>
    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var user = await GetByIdAsync(id, cancellationToken);
        if (user == null)
            return false;

        _context.Users.Remove(user);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}
