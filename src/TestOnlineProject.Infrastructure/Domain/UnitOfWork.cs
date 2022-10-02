using Microsoft.Azure.Cosmos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TestOnlineProject.Domain.SeedWork;

namespace TestOnlineProject.Infrastructure.Domain
{
    internal class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _context;
        private bool _hasActiveTransaction;
        
        public UnitOfWork(DbContext context)
        {
            _context = context;
        }
        public async Task ExecuteAsync(Func<Task> action, CancellationToken cancellationToken = default)
        {
            await action();
            await _context.SaveChangesAsync(cancellationToken);
            //if (_hasActiveTransaction)
            //{
            //    await action();
            //    return;
            //}   
            //var strategy = _context.Database.CreateExecutionStrategy();
            //await strategy.ExecuteAsync(async () =>
            //{
            //    await using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
            //    _hasActiveTransaction = true;
            //    try
            //    {
            //        await action();
            //        await _context.SaveChangesAsync(cancellationToken);
            //        await transaction.CommitAsync(cancellationToken);
            //    }
            //    catch (Exception)
            //    {
            //        await transaction.RollbackAsync(cancellationToken);
            //        throw;
            //    }
            //    finally
            //    {
            //        _hasActiveTransaction = false;
            //    }
            //});
        }

        public async Task<TResponse> ExecuteAsync<TResponse>(Func<Task<TResponse>> action, CancellationToken cancellationToken = default)
        {
            var result = await action();
            await _context.SaveChangesAsync(cancellationToken);

            return result;
            //if (_hasActiveTransaction) return action();

            //var strategy = _context.Database.CreateExecutionStrategy();

            //return strategy.ExecuteAsync(async () =>
            //{
            //    await using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
            //    _hasActiveTransaction = true;
            //    try
            //    {
            //        var result = await action();
            //        await _context.SaveChangesAsync(cancellationToken);
            //        await transaction.CommitAsync(cancellationToken);

            //        return result;
            //    }
            //    catch (Exception)
            //    {
            //        await transaction.RollbackAsync(cancellationToken);
            //        throw;
            //    }
            //    finally
            //    {
            //        _hasActiveTransaction = false;
            //    }
            //});
        }
    }
}
