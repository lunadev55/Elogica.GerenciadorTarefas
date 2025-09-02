using MediatR;
using Microsoft.Extensions.Logging;
using TaskManager.DeveloperEvaluation.Domain.Entities;
using TaskManager.DeveloperEvaluation.Domain.Repositories;

namespace TaskManager.DeveloperEvaluation.Application.Projects.CreateProject
{
    /// <summary>
    /// Handles the creation of a new <see cref="Project"/>.
    /// Validates the user, constructs the project entity, and persists it.
    /// </summary>
    public class CreateProjectHandler : IRequestHandler<CreateProjectCommand, CreateProjectResult>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IUserRepository _userRepository;
        private readonly ILogger<CreateProjectHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateProjectHandler"/> class.
        /// </summary>
        /// <param name="projectRepository">
        /// The repository for persisting <see cref="Project"/> entities.
        /// </param>
        /// <param name="userRepository">
        /// The repository for accessing <see cref="User"/> entities to validate the user.
        /// </param>
        /// <param name="logger">
        /// The logger for recording project creation events.
        /// </param>
        public CreateProjectHandler(
            IProjectRepository projectRepository,
            IUserRepository userRepository,
            ILogger<CreateProjectHandler> logger)
        {
            _projectRepository = projectRepository;
            _userRepository = userRepository;
            _logger = logger;
        }

        /// <summary>
        /// Processes a <see cref="CreateProjectCommand"/> by verifying the user exists,
        /// creating a new <see cref="Project"/>, and saving it to the database.
        /// </summary>
        /// <param name="request">
        /// The <see cref="CreateProjectCommand"/> containing project details.
        /// </param>
        /// <param name="cancellationToken">
        /// A token to observe while awaiting asynchronous operations.
        /// </param>
        /// <returns>
        /// A <see cref="CreateProjectResult"/> containing the newly created project's ID.
        /// </returns>
        /// <exception cref="KeyNotFoundException">
        /// Thrown if the specified user does not exist.
        /// </exception>
        public async Task<CreateProjectResult> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.UserId);
            if (user == null)
                throw new KeyNotFoundException($"User '{request.UserId}' not found");

            var project = new Project(
                Guid.NewGuid(),
                request.Name,
                request.Description,
                request.StartDate,
                request.EndDate,
                request.Status,
                request.UserId,
                DateTime.UtcNow
            );

            await _projectRepository.AddAsync(project);
            await _projectRepository.SaveChangesAsync(cancellationToken);

            _logger.LogInformation(
                "ProjectCreated | ProjectId={ProjectId} | Name={Name} | UserId={UserId} | Status={Status} | StartDate={StartDate} | EndDate={EndDate}",
                project.Id,
                project.Name,
                project.UserId,
                project.Status,
                project.StartDate,
                project.EndDate
            );

            return new CreateProjectResult { Id = project.Id };
        }
    }
}