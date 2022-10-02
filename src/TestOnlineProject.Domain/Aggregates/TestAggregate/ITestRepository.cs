using TestOnlineProject.Domain.SeedWork;

namespace TestOnlineProject.Domain.Aggregates.TestAggregate
{
    public interface ITestRepository : IRepository<Test, Guid>
    {
    }
}
