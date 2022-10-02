namespace TestOnlineProject.Infrastructure.CQRS.Queries
{
    public interface IQueryBus
    {
        Task<TResponse> SendAsync<TResponse>(IQuery<TResponse> query, CancellationToken cancellationToken = default);
    }
}
