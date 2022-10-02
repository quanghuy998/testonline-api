using TestOnlineProject.Domain.Aggregates.TestAggregate;
using TestOnlineProject.Infrastructure.CQRS.Queries;

namespace TestOnlineProject.Application.Queries.Tests
{
    public class GetAllTestsQuery : IQuery<IEnumerable<Test>>
    {

    }

    public class GetAllTestsQueryHandler : IQueryHandler<GetAllTestsQuery, IEnumerable<Test>>
    {
        private readonly ITestRepository _testRepository;

        public GetAllTestsQueryHandler(ITestRepository testRepository)
        {
            _testRepository = testRepository;
        }

        public async Task<IEnumerable<Test>> Handle(GetAllTestsQuery request, CancellationToken cancellationToken)
        {
             return await _testRepository.FindAllAsync(null, cancellationToken);
        }
    }
}
