using MediatR;
using Microsoft.Extensions.Logging;
using TaskManager.DeveloperEvaluation.Domain.Entities;
using TaskManager.DeveloperEvaluation.Domain.Repositories;
using Task = TaskManager.DeveloperEvaluation.Domain.Entities.Task;

namespace TaskManager.DeveloperEvaluation.Application.Tasks.CreateTask
{
    /// <summary>
    /// Handles the creation of a new <see cref="Task"/>.
    /// Validates the project and user, constructs the task entity, and persists it.
    /// </summary>
    public class CreateTaskHandler : IRequestHandler<CreateTaskCommand, CreateTaskResult>
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IUserRepository _userRepository;
        private readonly ILogger<CreateTaskHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateTaskHandler"/> class.
        /// </summary>
        /// <param name="taskRepository">
        /// The repository for persisting <see cref="Task"/> entities.
        /// </param>
        /// <param name="projectRepository">
        /// The repository for accessing <see cref="Project"/> entities to validate the project exists.
        /// </param>
        /// <param name="userRepository">
        /// The repository for accessing <see cref="User"/> entities to validate the assignee.
        /// </param>
        /// <param name="logger">
        /// The logger for recording task creation events.
        /// </param>
        public CreateTaskHandler(
            ITaskRepository taskRepository,
            IProjectRepository projectRepository,
            IUserRepository userRepository,
            ILogger<CreateTaskHandler> logger)
        {
            _taskRepository = taskRepository;
            _projectRepository = projectRepository;
            _userRepository = userRepository;
            _logger = logger;
        }

        /// <summary>
        /// Processes a <see cref="CreateTaskCommand"/> by verifying the project and user exist,
        /// creating a new <see cref="Task"/>, and saving it to the database.
        /// </summary>
        /// <param name="request">
        /// The <see cref="CreateTaskCommand"/> containing task details.
        /// </param>
        /// <param name="cancellationToken">
        /// A token to observe while awaiting asynchronous operations.
        /// </param>
        /// <returns>
        /// A <see cref="CreateTaskResult"/> containing the newly created task's ID.
        /// </returns>
        /// <exception cref="KeyNotFoundException">
        /// Thrown if the specified project or user does not exist.
        /// </exception>
        public async Task<CreateTaskResult> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
        {
            var project = await _projectRepository.GetByIdAsync(request.ProjectId, cancellationToken);
            if (project == null)
                throw new KeyNotFoundException($"Project '{request.ProjectId}' not found");

            var user = await _userRepository.GetByIdAsync(request.UserId);
            if (user == null)
                throw new KeyNotFoundException($"User '{request.UserId}' not found");

            var task = new Task(
                Guid.NewGuid(),
                request.Title,
                request.Description,
                request.DueDate,
                request.Status,
                request.Priority,
                request.ProjectId,
                request.UserId,
                DateTime.UtcNow
            );

            await _taskRepository.AddAsync(task);
            await _taskRepository.SaveChangesAsync(cancellationToken);

            _logger.LogInformation(
                "TaskCreated | TaskId={TaskId} | Title={Title} | ProjectId={ProjectId} | UserId={UserId} | Status={Status} | Priority={Priority} | DueDate={DueDate}",
                task.Id,
                task.Title,
                task.ProjectId,
                task.UserId,
                task.Status,
                task.Priority,
                task.DueDate
            );

            return new CreateTaskResult { Id = task.Id };
        }
    }
}