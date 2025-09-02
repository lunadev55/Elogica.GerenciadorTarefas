using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.DeveloperEvaluation.Application.Projects.CreateProject;
using TaskManager.DeveloperEvaluation.Application.Projects.DeleteProject;
using TaskManager.DeveloperEvaluation.Application.Projects.Queries.GetProjectById;
using TaskManager.DeveloperEvaluation.Application.Projects.Queries.GetProjectsList;
using TaskManager.DeveloperEvaluation.Application.Projects.UpdateProject;
using TaskManager.DeveloperEvaluation.WebApi.Common;
using TaskManager.DeveloperEvaluation.WebApi.Features.Projects.CreateProjectFeature;
using TaskManager.DeveloperEvaluation.WebApi.Features.Projects.DeleteProject;
using TaskManager.DeveloperEvaluation.WebApi.Features.Projects.GetProjectById;
using TaskManager.DeveloperEvaluation.WebApi.Features.Projects.GetProjectsList;
using TaskManager.DeveloperEvaluation.WebApi.Features.Projects.UpdateProject;

namespace TaskManager.DeveloperEvaluation.WebApi.Features.Projects;

/// <summary>
/// Controller for managing projects
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ProjectsController : BaseController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of ProjectsController
    /// </summary>
    /// <param name="mediator">The mediator instance</param>
    public ProjectsController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    /// <summary>
    /// GET /api/projects?_page=1&_size=10
    /// Retrieves a paginated list of projects
    /// </summary>
    [HttpGet]
    [Authorize(Roles = "Admin,Common")]
    [ProducesResponseType(typeof(ApiResponseWithData<GetProjectsListResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> GetList(
        [FromQuery(Name = "_page")] int page = 1,
        [FromQuery(Name = "_size")] int size = 10,
        CancellationToken cancellationToken = default)
    {
        var request = new GetProjectsListRequest { Page = page, Size = size };
        var validator = new GetProjectsListRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var query = _mapper.Map<GetProjectsListQuery>(request);
        var result = await _mediator.Send(query, cancellationToken);

        return Ok(new ApiResponseWithData<GetProjectsListResponse>
        {
            Success = true,
            Message = "Projects retrieved successfully",
            Data = _mapper.Map<GetProjectsListResponse>(result)
        });
    }


    /// <summary>
    /// GET /api/projects/{id}
    /// Retrieves a project by its ID
    /// </summary>
    [HttpGet("{id}", Name = nameof(GetProjectById))]
    [Authorize(Roles = "Admin,Common")]
    public async Task<IActionResult> GetProjectById([FromRoute] Guid id, CancellationToken cancellationToken)
    {       
        var request = new GetProjectByIdRequest { Id = id };
        var validator = new GetProjectByIdRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var query = _mapper.Map<GetProjectByIdQuery>(request);
        var result = await _mediator.Send(query, cancellationToken);

        return Ok(new ApiResponseWithData<GetProjectByIdResponse>
        {
            Success = true,
            Message = "Project retrieved successfully",
            Data = _mapper.Map<GetProjectByIdResponse>(result)
        });
    }

    /// <summary>
    /// POST /api/projects
    /// Creates a new project
    /// </summary>
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create([FromBody] CreateProjectRequest request, CancellationToken cancellationToken)
    {       
        var validator = new CreateProjectRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = _mapper.Map<CreateProjectCommand>(request);
        var response = await _mediator.Send(command, cancellationToken);

        return Created(string.Empty, new ApiResponseWithData<CreateProjectResponse>
        {
            Success = true,
            Message = "Project created successfully",
            Data = _mapper.Map<CreateProjectResponse>(response)
        });
    }

    /// <summary>
    /// PUT /api/projects/{id}
    /// Updates an existing project
    /// </summary>
    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateProjectRequest request, CancellationToken cancellationToken)
    {        
        var validator = new UpdateProjectRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = _mapper.Map<UpdateProjectCommand>(request);
        command.Id = id;

        var response = await _mediator.Send(command, cancellationToken);

        return Ok(new ApiResponseWithData<UpdateProjectResponse>
        {
            Success = true,
            Message = "Project updated successfully",
            Data = _mapper.Map<UpdateProjectResponse>(response)
        });        
    }

    /// <summary>
    /// DELETE /api/projects/{id}
    /// Deletes a project by its ID
    /// </summary>
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken cancellationToken)
    {       
        var request = new DeleteProjectRequest { Id = id };
        var validator = new DeleteProjectRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = _mapper.Map<DeleteProjectCommand>(request.Id);
        await _mediator.Send(command, cancellationToken);

        return Ok(new ApiResponse
        {
            Success = true,
            Message = "Project deleted successfully"
        });
    }
}