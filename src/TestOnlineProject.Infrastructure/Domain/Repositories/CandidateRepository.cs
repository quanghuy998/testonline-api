using TestOnlineProject.Domain.Aggregates.CandidateAggregate;
using TestOnlineProject.Infrastructure.Database;

namespace TestOnlineProject.Infrastructure.Domain.Repositories
{
    public class CandidateRepository : BaseRepository<Candidate, Guid>, ICandidateRepository
    {
        public CandidateRepository(AppDbContext context) : base(context)
        {

        }
    }
}
