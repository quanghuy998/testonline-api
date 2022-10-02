using TestOnlineProject.Infrastructure.CQRS.Commands;
using TestOnlineProject.Domain.Aggregates.SubmissionAggregate;

namespace TestOnlineProject.Application.Commands.Submissions
{
    public class DeleteSubmissionCommand : ICommand<Guid>
    {
        public Guid Id { get; init; }
    }

    public class DeleteSubmissionCommandHandler : ICommandHandler<DeleteSubmissionCommand, Guid>
    {
        private readonly ISubmissionRepository _submissionRepository;

        public DeleteSubmissionCommandHandler(ISubmissionRepository submissionRepository)
        {
            _submissionRepository = submissionRepository;
        }

        public async Task<CommandResult<Guid>> Handle(DeleteSubmissionCommand request, CancellationToken cancellationToken)
        {
            var submission = await _submissionRepository.FindOneAsync(request.Id, cancellationToken);
            if (submission == null) return CommandResult<Guid>.Error("The test of candidate does not exist.");
            await _submissionRepository.DeleteAsync(submission, cancellationToken);

            return CommandResult<Guid>.Success(submission.Id);
        }
    }
}
