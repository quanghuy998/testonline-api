using TestOnlineProject.Domain.Aggregates.CandidateAggregate;
using TestOnlineProject.Infrastructure.CQRS.Queries;

namespace TestOnlineProject.Application.Queries.Candidates
{
    public class GetCandidateDetailsQuery : IQuery<Candidate>
    {
        public Guid Id { get; init; }
    }

    public class GetCandidateDetailsQueryHandler : IQueryHandler<GetCandidateDetailsQuery, Candidate>
    {
        private readonly ICandidateRepository _candidateRepository;

        public GetCandidateDetailsQueryHandler(ICandidateRepository candidateRepository)
        {
            _candidateRepository = candidateRepository;
        }

        public async Task<Candidate> Handle(GetCandidateDetailsQuery request, CancellationToken cancellationToken)
        {
            return await _candidateRepository.FindOneAsync(request.Id, cancellationToken);
        }
    }
}
