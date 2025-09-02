using TaskManager.DeveloperEvaluation.Domain.Enums;
using TaskStatus = TaskManager.DeveloperEvaluation.Domain.Enums.TaskStatus;

namespace TaskManager.DeveloperEvaluation.Domain.Entities;

public class Task
{
    public Guid Id { get; private set; }
    public string Title { get; private set; }
    public string Description { get; private set; }
    public DateTime DueDate { get; private set; }
    public TaskStatus Status { get; private set; }
    public TaskPriority Priority { get; private set; }
    public Guid ProjectId { get; private set; }
    public Guid UserId { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    public virtual Project Project { get; private set; }

    protected Task() { }

    public Task(Guid id, string title, string description, DateTime dueDate, TaskStatus status,
               TaskPriority priority, Guid projectId, Guid userId, DateTime createdAt)
    {
        Id = id;
        Title = title;
        Description = description;
        DueDate = dueDate;
        Status = status;
        Priority = priority;
        ProjectId = projectId;
        UserId = userId;
        CreatedAt = createdAt;
    }

    public void Update(string title, string description, DateTime dueDate, TaskStatus status,
                      TaskPriority priority, DateTime updatedAt)
    {
        Title = title;
        Description = description;
        DueDate = dueDate;
        Status = status;
        Priority = priority;
        UpdatedAt = updatedAt;
    }
}