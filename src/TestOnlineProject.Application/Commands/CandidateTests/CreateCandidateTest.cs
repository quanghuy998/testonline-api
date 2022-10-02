using TestOnlineProject.Infrastructure.CQRS.Commands;
using TestOnlineProject.Domain.Aggregates.TestAggregate;
using TestOnlineProject.Domain.Aggregates.SubmissionAggregate;

namespace TestOnlineProject.Application.Commands.CandidateTests
{
    public class CreateCandidateTestCommand : ICommand<Guid>
    {
        public Guid TestId { get; init; }
        public Guid CandidateId { get; init; }
    }

    public class CreateCandidateTestCommandHandler : ICommandHandler<CreateCandidateTestCommand, Guid>
    {
        private readonly ICandidateTestRepository _candidateTestRepository;
        private readonly ITestRepository _testRepository;

        public CreateCandidateTestCommandHandler(ICandidateTestRepository candidateTestRepository, ITestRepository testRepository)
        {
            _candidateTestRepository = candidateTestRepository;
            _testRepository = testRepository;
        }

        public async Task<CommandResult<Guid>> Handle(CreateCandidateTestCommand request, CancellationToken cancellationToken)
        {
            var test = await _testRepository.FindOneAsync(request.TestId, cancellationToken);
            if (test is null) return CommandResult<Guid>.Error("Can not create a candidate test. Because the test does not exist.");

            if (test.Questions is null || test.Questions.Count == 0) return CommandResult<Guid>.Error("Can not create a candidate test. Because the test does not exist any question.");

            var answers = new List<Answer>();
            foreach(var question in test.Questions)
            {
                var answer = new Answer(question.Id);
                answers.Add(answer);
            }
            var candidateTest = new Submission(request.TestId, request.CandidateId, answers);
            await _candidateTestRepository.AddAsync(candidateTest, cancellationToken);

            return CommandResult<Guid>.Success(candidateTest.Id);
        }
    }
}
