using AutoMapper;
using TaskEntity = TaskManager.DeveloperEvaluation.Domain.Entities.Task;

namespace TaskManager.DeveloperEvaluation.Application.Tasks.Queries.GetTaskById
{
    public class GetTaskByIdProfile : Profile
    {
        public GetTaskByIdProfile()
        {
            CreateMap<TaskEntity, GetTaskByIdResult>()
                .ForMember(d => d.Status, o => o.MapFrom(s => s.Status.ToString()))
                .ForMember(d => d.Priority, o => o.MapFrom(s => s.Priority.ToString()));
        }
    }
}
