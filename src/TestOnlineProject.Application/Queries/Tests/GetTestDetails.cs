using TestOnlineProject.Domain.Aggregates.TestAggregate;
using TestOnlineProject.Infrastructure.CQRS.Queries;

namespace TestOnlineProject.Application.Queries.Tests
{
    public class GetTestDetailsQuery : IQuery<Test>
    {
        public Guid Id { get; set; }
    }

    public class GetTestDetailsQueryHandler : IQueryHandler<GetTestDetailsQuery, Test>
    {
        private readonly ITestRepository _testRepository;

        public GetTestDetailsQueryHandler(ITestRepository testRepository)
        {
            _testRepository = testRepository;
        }

        public async Task<Test> Handle(GetTestDetailsQuery request, CancellationToken cancellationToken)
        {
            return await _testRepository.FindOneAsync(request.Id, cancellationToken);
        }
    }
}
