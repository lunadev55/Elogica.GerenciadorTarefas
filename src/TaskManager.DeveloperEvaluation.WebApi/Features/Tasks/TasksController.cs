using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.DeveloperEvaluation.Application.Tasks.CreateTask;
using TaskManager.DeveloperEvaluation.Application.Tasks.DeleteTask;
using TaskManager.DeveloperEvaluation.Application.Tasks.Queries.GetTaskById;
using TaskManager.DeveloperEvaluation.Application.Tasks.Queries.GetTasksList;
using TaskManager.DeveloperEvaluation.Application.Tasks.UpdateTask;
using TaskManager.DeveloperEvaluation.WebApi.Common;

namespace TaskManager.DeveloperEvaluation.WebApi.Features.Tasks;

/// <summary>
/// Controller for managing tasks
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class TasksController : BaseController
{
    private readonly IMediator _mediator;

    /// <summary>
    /// Initializes a new instance of TasksController
    /// </summary>
    /// <param name="mediator">The mediator instance</param>
    public TasksController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// GET /api/tasks?_page=1&_size=10
    /// Retrieves a paginated list of tasks
    /// </summary>
    [HttpGet]
    [Authorize(Roles = "Admin,Common")]
    public async Task<IActionResult> GetList(
        [FromQuery(Name = "_page")] int page = 1,
        [FromQuery(Name = "_size")] int size = 10)
    {
        var query = new GetTasksListQuery { Page = page, Size = size };
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    /// <summary>
    /// GET /api/tasks/{id}
    /// Retrieves a task by its ID
    /// </summary>
    [HttpGet("{id}", Name = nameof(GetTaskById))]
    [Authorize(Roles = "Admin,Common")]
    public async Task<IActionResult> GetTaskById(Guid id)
    {
        var query = new GetTaskByIdQuery { Id = id };
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    /// <summary>
    /// POST /api/tasks
    /// Creates a new task
    /// </summary>
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create([FromBody] CreateTaskCommand command)
    {
        var result = await _mediator.Send(command);
        return CreatedAtRoute(nameof(GetTaskById), new { id = result.Id }, result);
    }

    /// <summary>
    /// PUT /api/tasks/{id}
    /// Updates an existing task
    /// </summary>
    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateTaskCommand command)
    {
        if (id != command.Id)
            return BadRequest("Id mismatch between route and payload.");

        var result = await _mediator.Send(command);
        return Ok(result);
    }

    /// <summary>
    /// DELETE /api/tasks/{id}
    /// Deletes a task by its ID
    /// </summary>
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var command = new DeleteTaskCommand { Id = id };
        var result = await _mediator.Send(command);
        return Ok(result);
    }
}
