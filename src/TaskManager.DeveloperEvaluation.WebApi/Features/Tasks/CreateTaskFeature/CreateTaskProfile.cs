using AutoMapper;
using TaskManager.DeveloperEvaluation.Application.Tasks.CreateTask;

namespace TaskManager.DeveloperEvaluation.WebApi.Features.Tasks.CreateTaskFeature;

/// <summary>
/// AutoMapper profile for CreateTask feature mappings.
/// </summary>
public class CreateTaskProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateTaskProfile"/> class.
    /// </summary>
    public CreateTaskProfile()
    {        
        CreateMap<CreateTaskRequest, CreateTaskCommand>();
                
        CreateMap<CreateTaskResult, CreateTaskResponse>()
            .ForMember(dest => dest.Message, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore());
    }
}