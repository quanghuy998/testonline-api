using TestOnlineProject.Domain.Aggregates.TestAggregate;
using TestOnlineProject.Infrastructure.CQRS.Commands;

namespace TestOnlineProject.Application.Commands.Tests
{
    public class AddChoiceToQuestionInTestCommand : ICommand<Guid>
    {
        public Guid TestId { get; init; }
        public Guid QuestionId { get; init; }
        public string ChoiceText { get; init; }
        public bool IsCorrect { get; init; }
    }

    public class AddChoiceToQuestionInTestCommandHandler : ICommandHandler<AddChoiceToQuestionInTestCommand, Guid>
    {
        private readonly ITestRepository _testRepository;

        public AddChoiceToQuestionInTestCommandHandler(ITestRepository testRepository)
        {
            _testRepository = testRepository;
        }

        public async Task<CommandResult<Guid>> Handle(AddChoiceToQuestionInTestCommand request, CancellationToken cancellationToken)
        {
            var test = await _testRepository.FindOneAsync(request.TestId, cancellationToken);
            if (test is null) return CommandResult<Guid>.Error("The test does not exist.");

            var question = test.Questions.Find(x => x.Id == request.QuestionId);
            if (question is null) return CommandResult<Guid>.Error("The question does not exist in this test.");

            if (question.QuestionType == QuestionType.Code) return CommandResult<Guid>.Error("The question type is not multip choice.");

            var choice = new Choice(request.QuestionId, request.ChoiceText, request.IsCorrect);
            test.AddChoice(choice);

            await _testRepository.SaveAsync(test, cancellationToken);

            return CommandResult<Guid>.Success(choice.Id);

        }
    }
}
