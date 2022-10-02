using TestOnlineProject.Infrastructure.CQRS.Commands;
using TestOnlineProject.Domain.Aggregates.SubmissionAggregate;

namespace TestOnlineProject.Application.Commands.CandidateTests
{
    public class MarkTheAnswerInCandidateTestCommand : ICommand<Guid>
    {
        public Guid CandidateTestId { get; init; }
        public Guid AnswerId { get; init; }
        public int Score { get; init; }
    }

    public class MarkTheAnswerInCandidateTestCommandHandler : ICommandHandler<MarkTheAnswerInCandidateTestCommand, Guid>
    {
        private readonly ICandidateTestRepository _candidateTestRepository;

        public MarkTheAnswerInCandidateTestCommandHandler(ICandidateTestRepository candidateTestRepository)
        {
            _candidateTestRepository = candidateTestRepository;
        }

        public async Task<CommandResult<Guid>> Handle(MarkTheAnswerInCandidateTestCommand request, CancellationToken cancellationToken)
        {
            var candidateTest = await _candidateTestRepository.FindOneAsync(request.CandidateTestId, cancellationToken);
            if (candidateTest is null) return CommandResult<Guid>.Error("The test of candidate does not exist.");

            var answer = candidateTest.Answers.Find(x => x.Id == request.AnswerId);
            if (answer is null) return CommandResult<Guid>.Error("The answer does not exist in this candidate test.");

            candidateTest.MarkTheAnswerInCandidateTest(request.AnswerId, request.Score);
            await _candidateTestRepository.SaveAsync(candidateTest, cancellationToken);

            return CommandResult<Guid>.Success(answer.Id);
        }
    }
}
