using TestOnlineProject.Domain.Aggregates.SubmissionAggregate;
using TestOnlineProject.Infrastructure.Database;

namespace TestOnlineProject.Infrastructure.Domain.Repositories
{
    public class CandidateTestRepository : BaseRepository<Submission, Guid>, ICandidateTestRepository
    {
        public CandidateTestRepository(AppDbContext context) : base(context)
        {

        }
    }
        
}
