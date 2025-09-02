using AutoMapper;
using TaskManager.DeveloperEvaluation.Application.Projects.DeleteProject;

namespace TaskManager.DeveloperEvaluation.WebApi.Features.Projects.DeleteProject;

/// <summary>
/// AutoMapper profile for DeleteProject feature mappings.
/// </summary>
public class DeleteProjectProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteProjectProfile"/> class.
    /// </summary>
    public DeleteProjectProfile()
    {        
        CreateMap<Guid, DeleteProjectCommand>()
            .ConstructUsing(id => new DeleteProjectCommand(id));
    }
}