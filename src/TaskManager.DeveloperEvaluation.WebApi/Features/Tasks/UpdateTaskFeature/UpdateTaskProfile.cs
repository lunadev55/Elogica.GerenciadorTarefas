using AutoMapper;
using TaskManager.DeveloperEvaluation.Application.Tasks.UpdateTask;

namespace TaskManager.DeveloperEvaluation.WebApi.Features.Tasks.UpdateTaskFeature;

/// <summary>
/// AutoMapper profile for UpdateTask feature mappings.
/// </summary>
public class UpdateTaskProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateTaskProfile"/> class.
    /// </summary>
    public UpdateTaskProfile()
    {        
        CreateMap<UpdateTaskRequest, UpdateTaskCommand>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());
                
        CreateMap<UpdateTaskResult, UpdateTaskResponse>()
            .ForMember(dest => dest.Message, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());
    }
}