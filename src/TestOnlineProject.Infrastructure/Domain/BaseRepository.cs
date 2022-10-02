using Microsoft.EntityFrameworkCore;
using TestOnlineProject.Domain.SeedWork;

namespace TestOnlineProject.Infrastructure.Domain
{
    public abstract class BaseRepository<TAggregateRoot, TId> : IRepository<TAggregateRoot, TId>
                    where TAggregateRoot : AggregateRoot<TId>
    {
        protected DbContext DbContext { get; }
        protected BaseRepository(DbContext context)
        {
            DbContext = context ?? throw new ArgumentNullException(nameof(context));
        }
        public Task<bool> ExistsAsync(TId id, CancellationToken cancellationToken = default)
        {
            return DbContext.Set<TAggregateRoot>().AnyAsync(x => x.Id.Equals(id), cancellationToken);
        }

        public Task<bool> ExistsAsync(IBaseSpecification<TAggregateRoot> specification, CancellationToken cancellationToken = default)
        {
            return DbContext.Set<TAggregateRoot>().AnyAsync(specification.Expression, cancellationToken);
        }

        public Task<TAggregateRoot> FindOneAsync(TId id, CancellationToken cancellationToken = default)
        {
            return DbContext.Set<TAggregateRoot>().FirstOrDefaultAsync(x => x.Id.Equals(id), cancellationToken);
        }

        public Task<TAggregateRoot> FindOneAsync(IBaseSpecification<TAggregateRoot> specification, CancellationToken cancellationToken = default)
        {
            if (specification == null) return Task.FromResult(default(TAggregateRoot));
            var queryable = DbContext.Set<TAggregateRoot>().AsQueryable();
            var queryableWithInclude = specification.Includes.Aggregate(queryable, (current, include) => current.Include(include));

            return queryableWithInclude.FirstOrDefaultAsync(specification.Expression, cancellationToken);
        }

        public Task<IEnumerable<TAggregateRoot>> FindAllAsync(IBaseSpecification<TAggregateRoot> specification, CancellationToken cancellationToken = default)
        {
            var queryable = DbContext.Set<TAggregateRoot>().AsQueryable();
            if (specification == null) return Task.FromResult(queryable.AsNoTracking().AsEnumerable());

            var queryableWithInclude = specification.Includes
                .Aggregate(queryable, (current, include) => current.Include(include));
            var result = queryableWithInclude
                .Where(specification.Expression)
                .AsNoTracking()
                .AsEnumerable();
            return Task.FromResult(result);
        }
        public async Task AddAsync(TAggregateRoot aggregate, CancellationToken cancellationToken)
        {
            await DbContext.Set<TAggregateRoot>().AddAsync(aggregate, cancellationToken);
        }

        public Task SaveAsync(TAggregateRoot aggregate, CancellationToken cancellationToken)
        {
            DbContext.Set<TAggregateRoot>().Update(aggregate);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(TAggregateRoot aggregate, CancellationToken cancellationToken)
        {
            DbContext.Set<TAggregateRoot>().Remove(aggregate);
            return Task.CompletedTask;
        }
    }
}
