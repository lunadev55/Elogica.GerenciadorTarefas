using Microsoft.EntityFrameworkCore;
using System.Reflection;
using TaskManager.DeveloperEvaluation.Domain.Entities;
using Task = TaskManager.DeveloperEvaluation.Domain.Entities.Task;

namespace TaskManager.DeveloperEvaluation.ORM;

/// <summary>
/// Represents the Entity Framework Core database context for the Developer Evaluation application.
/// Contains DbSet properties for Users, Projects, and Tasks.
/// </summary>
public class DefaultContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<Task> Tasks { get; set; }

    public DefaultContext(DbContextOptions<DefaultContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {        
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());        

        base.OnModelCreating(modelBuilder);
    }
}