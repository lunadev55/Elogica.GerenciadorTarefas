using TaskManager.DeveloperEvaluation.Domain.Entities;

namespace TaskManager.DeveloperEvaluation.Domain.Events
{
    public class UserRegisteredEvent
    {
        public User User { get; }

        public UserRegisteredEvent(User user)
        {
            User = user;
        }
    }
}
