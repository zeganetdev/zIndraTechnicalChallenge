using System.Linq.Expressions;

namespace zIndraTechnicalChallenge.Domain.MainContext.SeedWork
{
    public interface ISpecification<T>
    {
        Expression<Func<T, bool>> SpecExpression { get; }
        bool IsSatisfiedBy(T obj);
    }
}
