using TestOnlineProject.Domain.Aggregates.SubmissionAggregate;
using TestOnlineProject.Infrastructure.CQRS.Queries;

namespace TestOnlineProject.Application.Queries.Submissions
{
    public class GetAllSubmissionsQuery : IQuery<IEnumerable<Submission>>
    {

    }

    public class GetAllSubmissionsQueryHandler : IQueryHandler<GetAllSubmissionsQuery, IEnumerable<Submission>>
    {
        private readonly ISubmissionRepository _submissionRepository;

        public GetAllSubmissionsQueryHandler(ISubmissionRepository submissionRepository)
        {
            _submissionRepository = submissionRepository;
        }

        public async Task<IEnumerable<Submission>> Handle(GetAllSubmissionsQuery request, CancellationToken cancellationToken)
        { 
            return await _submissionRepository.FindAllAsync(null , cancellationToken);
        }
    }
}
