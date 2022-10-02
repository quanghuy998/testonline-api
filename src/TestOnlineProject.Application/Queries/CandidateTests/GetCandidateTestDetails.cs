using TestOnlineProject.Domain.Aggregates.SubmissionAggregate;
using TestOnlineProject.Infrastructure.CQRS.Queries;

namespace TestOnlineProject.Application.Queries.CandidateTests
{
    public class GetCandidateTestDetailsQuery : IQuery<Submission>
    {
        public Guid Id { get; init; }
    }

    public class GetCandidateTestDetailsQueryHandler : IQueryHandler<GetCandidateTestDetailsQuery, Submission>
    {
        private readonly ICandidateTestRepository _candidateTestRepository;

        public GetCandidateTestDetailsQueryHandler(ICandidateTestRepository candidateTestRepository)
        {
            _candidateTestRepository = candidateTestRepository;
        }

        public async Task<Submission> Handle(GetCandidateTestDetailsQuery request, CancellationToken cancellationToken)
        {
            return await _candidateTestRepository.FindOneAsync(request.Id, cancellationToken);
        }
    }
}
