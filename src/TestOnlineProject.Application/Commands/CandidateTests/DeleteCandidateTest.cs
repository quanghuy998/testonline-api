using TestOnlineProject.Infrastructure.CQRS.Commands;
using TestOnlineProject.Domain.Aggregates.SubmissionAggregate;

namespace TestOnlineProject.Application.Commands.CandidateTests
{
    public class DeleteCandidateTestCommand : ICommand<Guid>
    {
        public Guid Id { get; init; }
    }

    public class DeleteCandidateTestCommandHandler : ICommandHandler<DeleteCandidateTestCommand, Guid>
    {
        private readonly ICandidateTestRepository _candidateTestRepository;

        public DeleteCandidateTestCommandHandler(ICandidateTestRepository candidateTestRepository)
        {
            _candidateTestRepository = candidateTestRepository;
        }

        public async Task<CommandResult<Guid>> Handle(DeleteCandidateTestCommand request, CancellationToken cancellationToken)
        {
            var candidateTest = await _candidateTestRepository.FindOneAsync(request.Id, cancellationToken);
            if (candidateTest == null) return CommandResult<Guid>.Error("The test of candidate does not exist.");
            await _candidateTestRepository.DeleteAsync(candidateTest, cancellationToken);

            return CommandResult<Guid>.Success(candidateTest.Id);
        }
    }
}
