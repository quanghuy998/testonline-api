using System.Linq.Expressions;

namespace TestOnlineProject.Domain.SeedWork
{
    public sealed class BaseSpecification<T> : IBaseSpecification<T>
    {
        public Expression<Func<T, bool>> Expression { get; }
        public List<Expression<Func<T, object>>> Includes { get; }

        public BaseSpecification(Expression<Func<T, bool>> expression)
        {
            Expression = expression;
            Includes = new();
        }
    }
}
