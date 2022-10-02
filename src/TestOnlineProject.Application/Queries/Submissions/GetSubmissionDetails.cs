using TestOnlineProject.Domain.Aggregates.SubmissionAggregate;
using TestOnlineProject.Infrastructure.CQRS.Queries;

namespace TestOnlineProject.Application.Queries.Submissions
{
    public class GetSubmissionDetailsQuery : IQuery<Submission>
    {
        public Guid Id { get; init; }
    }

    public class GetSubmissionDetailsQueryHandler : IQueryHandler<GetSubmissionDetailsQuery, Submission>
    {
        private readonly ISubmissionRepository _submissionRepository;

        public GetSubmissionDetailsQueryHandler(ISubmissionRepository submissionRepository)
        {
            _submissionRepository = submissionRepository;
        }

        public async Task<Submission> Handle(GetSubmissionDetailsQuery request, CancellationToken cancellationToken)
        {
            return await _submissionRepository.FindOneAsync(request.Id, cancellationToken);
        }
    }
}
