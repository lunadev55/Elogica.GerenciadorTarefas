using AutoMapper;
using Task = TaskManager.DeveloperEvaluation.Domain.Entities.Task;

namespace TaskManager.DeveloperEvaluation.Application.Tasks.UpdateTask;

/// <summary>
/// AutoMapper profile for mapping between <see cref="UpdateTaskCommand"/> 
/// DTOs and domain <see cref="Task"/> entities.
/// </summary>
public class UpdateTaskProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateTaskProfile"/> class,
    /// configuring mappings for updating existing tasks.
    /// </summary>
    public UpdateTaskProfile()
    {
        CreateMap<UpdateTaskCommand, Task>()
            .ForMember(
                dest => dest.Id,
                opt => opt.MapFrom(src => src.Id))
            .ForMember(
                dest => dest.CreatedAt,
                opt => opt.Ignore())
            .ForMember(
                dest => dest.UpdatedAt,
                opt => opt.MapFrom(src => DateTime.UtcNow));
    }
}