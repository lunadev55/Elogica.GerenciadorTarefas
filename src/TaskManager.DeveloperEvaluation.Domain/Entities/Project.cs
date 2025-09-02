using TaskManager.DeveloperEvaluation.Domain.Enums;

namespace TaskManager.DeveloperEvaluation.Domain.Entities;

public class Project
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public DateTime StartDate { get; private set; }
    public DateTime? EndDate { get; private set; }
    public ProjectStatus Status { get; private set; }
    public Guid UserId { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    public virtual ICollection<Task> Tasks { get; private set; } = new List<Task>();

    protected Project() { }

    public Project(Guid id, string name, string description, DateTime startDate, DateTime? endDate,
                  ProjectStatus status, Guid userId, DateTime createdAt)
    {
        Id = id;
        Name = name;
        Description = description;
        StartDate = startDate;
        EndDate = endDate;
        Status = status;
        UserId = userId;
        CreatedAt = createdAt;
    }

    public void Update(string name, string description, DateTime startDate, DateTime? endDate,
                      ProjectStatus status, DateTime updatedAt)
    {
        Name = name;
        Description = description;
        StartDate = startDate;
        EndDate = endDate;
        Status = status;
        UpdatedAt = updatedAt;
    }
}