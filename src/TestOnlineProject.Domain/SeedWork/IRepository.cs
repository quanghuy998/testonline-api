namespace TestOnlineProject.Domain.SeedWork
{
    public interface IRepository<TAggregateRoot, TId> where TAggregateRoot : AggregateRoot<TId>
    {
        Task<bool> ExistsAsync(TId id, CancellationToken cancellationToken = default);
        
        Task<bool> ExistsAsync(IBaseSpecification<TAggregateRoot> specification, CancellationToken cancellationToken = default);

        Task<TAggregateRoot> FindOneAsync(TId id, CancellationToken cancellationToken = default);

        Task<TAggregateRoot> FindOneAsync(IBaseSpecification<TAggregateRoot> specification, CancellationToken cancellationToken = default);

        Task<IEnumerable<TAggregateRoot>> FindAllAsync(IBaseSpecification<TAggregateRoot> specification, CancellationToken cancellationToken = default);

        Task AddAsync(TAggregateRoot agggregate, CancellationToken cancellationToken);

        Task SaveAsync(TAggregateRoot agggregate, CancellationToken cancellationToken);

        Task DeleteAsync(TAggregateRoot agggregate, CancellationToken cancellationToken);
    }
}
