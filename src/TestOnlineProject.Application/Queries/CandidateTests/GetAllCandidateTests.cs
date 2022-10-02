using TestOnlineProject.Domain.Aggregates.CandidateAggregate;
using TestOnlineProject.Domain.Aggregates.SubmissionAggregate;
using TestOnlineProject.Infrastructure.CQRS.Queries;

namespace TestOnlineProject.Application.Queries.CandidateTests
{
    public class GetAllCandidateTestsQuery : IQuery<IEnumerable<Submission>>
    {

    }

    public class GetAllCandidateTestsQueryHandler : IQueryHandler<GetAllCandidateTestsQuery, IEnumerable<Submission>>
    {
        private readonly ICandidateTestRepository _candidateTestRepository;

        public GetAllCandidateTestsQueryHandler(ICandidateTestRepository candidateTestRepository)
        {
            _candidateTestRepository = candidateTestRepository;
        }

        public async Task<IEnumerable<Submission>> Handle(GetAllCandidateTestsQuery request, CancellationToken cancellationToken)
        { 
            return await _candidateTestRepository.FindAllAsync(null , cancellationToken);
        }
    }
}
