using TestOnlineProject.Domain.Aggregates.TestAggregate;
using TestOnlineProject.Infrastructure.CQRS.Commands;

namespace TestOnlineProject.Application.Commands.Tests
{
    public class AddQuestionToTestCommand : ICommand<Guid>
    {
        public Guid TestId { get; init; }
        public string QuestionText { get; init; }
        public int TimeLimit { get; init; }
        public int Point { get; init; }
        public QuestionType QuestionType { get; init; }
    }

    public class AddQuestionToTestCommandHandler : ICommandHandler<AddQuestionToTestCommand, Guid>
    {
        private readonly ITestRepository _testRepository;

        public AddQuestionToTestCommandHandler(ITestRepository testRepository)
        {
            _testRepository = testRepository;
        }

        public async Task<CommandResult<Guid>> Handle(AddQuestionToTestCommand request, CancellationToken cancellationToken)
        {
            var test = await _testRepository.FindOneAsync(request.TestId, cancellationToken);
            if (test == null) return CommandResult<Guid>.Error("The test does not exist.");

            var question = new Question(request.QuestionText, request.Point, request.TimeLimit, request.QuestionType);
            question.Id = Guid.NewGuid();
            test.AddQuestion(question);

            await _testRepository.SaveAsync(test, cancellationToken);

            return CommandResult<Guid>.Success(question.Id);
        }
    }
}
