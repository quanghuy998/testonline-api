using TestOnlineProject.Infrastructure.CQRS.Commands;
using TestOnlineProject.Domain.Aggregates.SubmissionAggregate;

namespace TestOnlineProject.Application.Commands.CandidateTests
{
    public class SubmitAnswersCommand : ICommand<Guid>
    {
        public Guid CandidateTestId { get; init; }
        public List<AnswerData> AnswerDatas { get; init; }
    }

    public class SubmitAnswersCommandHandler : ICommandHandler<SubmitAnswersCommand, Guid>
    {
        private readonly ICandidateTestRepository _candidateTestRepository;

        public SubmitAnswersCommandHandler(ICandidateTestRepository candidateTestRepository)
        {
            _candidateTestRepository = candidateTestRepository;
        }

        public async Task<CommandResult<Guid>> Handle(SubmitAnswersCommand request, CancellationToken cancellationToken)
        {
            var candidateTest = await _candidateTestRepository.FindOneAsync(request.CandidateTestId, cancellationToken);
            if (candidateTest is null) return CommandResult<Guid>.Error("The test of candidate does not exist.");

            if (candidateTest.FinishedDate != default(DateTime)) return CommandResult<Guid>.Error("Answers is already exists.");

            foreach(var requestData in request.AnswerDatas)
            {
                var result = candidateTest.Answers.Find(x => x.Id == requestData.AnswerId);
                if (result is null) return CommandResult<Guid>.Error($"The answers with id {requestData.AnswerId} does not exist in this test.");
            }
            candidateTest.AddAnswers(request.AnswerDatas);
            await _candidateTestRepository.SaveAsync(candidateTest, cancellationToken);

            return CommandResult<Guid>.Success(candidateTest.Id);
        }
    }
}
