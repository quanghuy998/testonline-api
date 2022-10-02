using TestOnlineProject.Domain.Aggregates.TestAggregate;
using TestOnlineProject.Infrastructure.CQRS.Commands;

namespace TestOnlineProject.Application.Commands.Tests
{
    public class RemoveChoiceFromQuestionInTestCommand : ICommand<Guid>
    {
        public Guid TestId { get; init; }
        public Guid QuestionId { get; init; }
        public Guid ChoiceId { get; init; }
    }

    public class RemoveChoiceFromQuestionInTestCommandHandler : ICommandHandler<RemoveChoiceFromQuestionInTestCommand, Guid>
    {
        private readonly ITestRepository _testRepository;

        public RemoveChoiceFromQuestionInTestCommandHandler(ITestRepository testRepository)
        {
            _testRepository = testRepository;
        }

        public async Task<CommandResult<Guid>> Handle(RemoveChoiceFromQuestionInTestCommand request, CancellationToken cancellationToken)
        {
            var test = await _testRepository.FindOneAsync(request.TestId, cancellationToken);
            if (test is null) return CommandResult<Guid>.Error("The test does not exist.");

            var question = test.Questions.Find(x => x.Id == request.QuestionId);
            if (question is null) return CommandResult<Guid>.Error("The question does not exist in this test.");

            var choice = question.Choices.Find(x => x.Id == request.ChoiceId);
            if (choice is null) return CommandResult<Guid>.Error("The choice does not exist in this question.");
            test.RemoveChoice(choice);

            await _testRepository.SaveAsync(test, cancellationToken);

            return CommandResult<Guid>.Success(choice.Id);
        }
    }
}
