using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestOnlineProject.Domain.Aggregates.TestAggregate;
using TestOnlineProject.Infrastructure.CQRS.Commands;

namespace TestOnlineProject.Application.Commands.Tests
{
    public class UpdateQuestionInTestCommand : ICommand<Guid>
    {
        public Guid TestId { get; init; }
        public Guid QuestionId { get; init; }
        public string QuestionText { get; init; }
        public int TimeLimit { get; init; }
        public int Point { get; init; }
        public QuestionType QuestionType { get; init; }
    }

    public class UpdateQuestionInTestCommandHandler : ICommandHandler<UpdateQuestionInTestCommand, Guid>
    {
        private readonly ITestRepository _testRepository;

        public UpdateQuestionInTestCommandHandler(ITestRepository testRepository)
        {
            _testRepository = testRepository;
        }

        public async Task<CommandResult<Guid>> Handle(UpdateQuestionInTestCommand request, CancellationToken cancellationToken)
        {
            var test = await _testRepository.FindOneAsync(request.TestId, cancellationToken);
            if (test is null) return CommandResult<Guid>.Error("The test does not exist.");

            var question = test.Questions.Find(x => x.Id == request.QuestionId);
            if (question is null) return CommandResult<Guid>.Error("The question does not exist in this test.");

            question.UpdateQuestion(request.QuestionText, request.QuestionType, request.Point, request.TimeLimit);

            await _testRepository.SaveAsync(test, cancellationToken);

            return CommandResult<Guid>.Success(question.Id);
        }
    }
}
