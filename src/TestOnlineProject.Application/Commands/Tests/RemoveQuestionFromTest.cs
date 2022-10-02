using TestOnlineProject.Domain.Aggregates.TestAggregate;
using TestOnlineProject.Infrastructure.CQRS.Commands;

namespace TestOnlineProject.Application.Commands.Tests
{
    public class RemoveQuestionFromTestCommand : ICommand<Guid>
    {
        public Guid TestId { get; init; }
        public Guid QuestionId { get; init; }
    }

    public class RemoveQuestionFromTestCommandHandler : ICommandHandler<RemoveQuestionFromTestCommand, Guid>
    {
        private readonly ITestRepository _testRepository;

        public RemoveQuestionFromTestCommandHandler(ITestRepository testRepository)
        {
            _testRepository = testRepository;
        }
        public async Task<CommandResult<Guid>> Handle(RemoveQuestionFromTestCommand request, CancellationToken cancellationToken)
        {
            var test = await _testRepository.FindOneAsync(request.TestId, cancellationToken);
            if (test is null) return CommandResult<Guid>.Error("The test does not exist.");

            var question = test.Questions.Find(x => x.Id == request.QuestionId);
            if (question is null) return CommandResult<Guid>.Error("The question does not exist in this test.");

            test.RemoveQuestion(request.QuestionId);
            await _testRepository.SaveAsync(test, cancellationToken);

            return CommandResult<Guid>.Success(question.Id);
        }
    }
}
