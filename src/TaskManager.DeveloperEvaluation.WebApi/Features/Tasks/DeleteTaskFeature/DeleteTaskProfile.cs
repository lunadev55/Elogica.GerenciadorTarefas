using AutoMapper;
using TaskManager.DeveloperEvaluation.Application.Tasks.DeleteTask;

namespace TaskManager.DeveloperEvaluation.WebApi.Features.Tasks.DeleteTaskFeature;

/// <summary>
/// AutoMapper profile for DeleteTask feature mappings.
/// </summary>
public class DeleteTaskProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteTaskProfile"/> class.
    /// </summary>
    public DeleteTaskProfile()
    {        
        CreateMap<DeleteTaskResult, DeleteTaskResponse>()
            .ForMember(dest => dest.Message, opt => opt.Ignore())
            .ForMember(dest => dest.DeletedAt, opt => opt.Ignore());
    }
}