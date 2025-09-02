namespace TaskManager.DeveloperEvaluation.Domain.Specifications;

public interface ISpecification<T>
{
    bool IsSatisfiedBy(T entity);
}
