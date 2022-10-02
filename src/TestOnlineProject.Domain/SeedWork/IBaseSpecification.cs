using System.Linq.Expressions;

namespace TestOnlineProject.Domain.SeedWork
{
    public interface IBaseSpecification<T>
    {
        Expression<Func<T, bool>> Expression { get; }
        List<Expression<Func<T, object>>> Includes { get; }
    }
}
