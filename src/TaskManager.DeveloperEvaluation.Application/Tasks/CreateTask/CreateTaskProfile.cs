using AutoMapper;
using Task = TaskManager.DeveloperEvaluation.Domain.Entities.Task;

namespace TaskManager.DeveloperEvaluation.Application.Tasks.CreateTask;

/// <summary>
/// AutoMapper profile for mapping between CreateTask DTOs and domain entities.
/// </summary>
public class CreateTaskProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateTaskProfile"/> class,
    /// configuring mappings from <see cref="CreateTaskCommand"/> to <see cref="Task"/>.
    /// </summary>
    public CreateTaskProfile()
    {
        CreateMap<CreateTaskCommand, Task>()
            .ConstructUsing(cmd => new Task(
                Guid.NewGuid(),
                cmd.Title,
                cmd.Description,
                cmd.DueDate,
                cmd.Status,
                cmd.Priority,
                cmd.ProjectId,
                cmd.UserId,
                DateTime.UtcNow));
    }
}