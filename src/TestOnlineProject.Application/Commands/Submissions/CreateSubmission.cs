using TestOnlineProject.Infrastructure.CQRS.Commands;
using TestOnlineProject.Domain.Aggregates.TestAggregate;
using TestOnlineProject.Domain.Aggregates.SubmissionAggregate;

namespace TestOnlineProject.Application.Commands.Submissions
{
    public class AnswerData
    {
        public Guid QuestionId { get; init; }
        public string QuestionText { get; init; }
        public string AnswerText { get; init; }
    }

    public class CreateSubmissionCommand : ICommand<Guid>
    {
        public Guid TestId { get; init; }
        public Guid CandidateId { get; init; }
        public List<AnswerData> AnswerDatas { get; init; }
    }

    public class CreateSubmissionCommandHandler : ICommandHandler<CreateSubmissionCommand, Guid>
    {
        private readonly ISubmissionRepository _submissionRepository;
        private readonly ITestRepository _testRepository;

        public CreateSubmissionCommandHandler(ISubmissionRepository submissionRepository, ITestRepository testRepository)
        {
            _submissionRepository = submissionRepository;
            _testRepository = testRepository;
        }

        public async Task<CommandResult<Guid>> Handle(CreateSubmissionCommand request, CancellationToken cancellationToken)
        {
            var test = await _testRepository.FindOneAsync(request.TestId, cancellationToken);
            if (test is null) return CommandResult<Guid>.Error("Can not create a candidate test. Because the test does not exist.");

            var answers = new List<Answer>();
            foreach(var data in request.AnswerDatas)
            {
                if(test.Questions.Exists(question => question.Id == data.QuestionId))
                {
                    answers.Add(new Answer(data.QuestionText, data.AnswerText));
                }
            }
            var submission = new Submission(request.TestId, request.CandidateId, answers);
            await _submissionRepository.AddAsync(submission, cancellationToken);

            return CommandResult<Guid>.Success(submission.Id);
        }

    }
}
