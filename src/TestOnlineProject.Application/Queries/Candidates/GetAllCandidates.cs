using TestOnlineProject.Domain.Aggregates.CandidateAggregate;
using TestOnlineProject.Infrastructure.CQRS.Queries;

namespace TestOnlineProject.Application.Queries.Candidates
{
    public class GetAllCandidatesQuery : IQuery<IEnumerable<Candidate>>
    {

    }

    public class GetAllCandidatesQueryHandler : IQueryHandler<GetAllCandidatesQuery, IEnumerable<Candidate>>
    {
        private readonly ICandidateRepository _candidateRepository;

        public GetAllCandidatesQueryHandler(ICandidateRepository candidateRepository)
        {
            _candidateRepository = candidateRepository;
        }

        public async Task<IEnumerable<Candidate>> Handle(GetAllCandidatesQuery request, CancellationToken cancellationToken)
        {
            return await _candidateRepository.FindAllAsync(null, cancellationToken);
        }
    }
}
