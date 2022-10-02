using TestOnlineProject.Domain.Aggregates.SubmissionAggregate;
using TestOnlineProject.Infrastructure.Database;

namespace TestOnlineProject.Infrastructure.Domain.Repositories
{
    public class SubmissionRepository : BaseRepository<Submission, Guid>, ISubmissionRepository
    {
        public SubmissionRepository(AppDbContext context) : base(context)
        {

        }
    }
        
}
