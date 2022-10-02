using TestOnlineProject.Domain.Aggregates.TestAggregate;
using TestOnlineProject.Domain.SeedWork;
using TestOnlineProject.Infrastructure.Database;

namespace TestOnlineProject.Infrastructure.Domain.Repositories
{
    public class TestRepository : BaseRepository<Test, Guid>, ITestRepository
    {
        public TestRepository(AppDbContext context) : base(context)
        {

        }

        Task<Test> IRepository<Test, Guid>.FindOneAsync(Guid id, CancellationToken cancellationToken)
        {
            var spec = new BaseSpecification<Test>(p => p.Id == id);
            spec.Includes.Add(p => p.Questions);

            return FindOneAsync(spec, cancellationToken);
        }
    }
}
