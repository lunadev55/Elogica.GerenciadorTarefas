using AutoMapper;
using TaskEntity = TaskManager.DeveloperEvaluation.Domain.Entities.Task;

namespace TaskManager.DeveloperEvaluation.Application.Tasks.Queries.GetTasksList
{
    public class GetTasksListProfile : Profile
    {
        public GetTasksListProfile()
        {
            CreateMap<TaskEntity, TaskListItem>()
                .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
                .ForMember(d => d.Title, o => o.MapFrom(s => s.Title))
                .ForMember(d => d.DueDate, o => o.MapFrom(s => s.DueDate))
                .ForMember(d => d.Status, o => o.MapFrom(s => s.Status.ToString()))
                .ForMember(d => d.Priority, o => o.MapFrom(s => s.Priority.ToString()));
        }
    }
}
