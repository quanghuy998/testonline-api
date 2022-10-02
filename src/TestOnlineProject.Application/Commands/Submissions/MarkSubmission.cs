using TestOnlineProject.Infrastructure.CQRS.Commands;
using TestOnlineProject.Domain.Aggregates.SubmissionAggregate;

namespace TestOnlineProject.Application.Commands.Submissions
{
    public class AnswerScoreData
    {
        public Guid AnswerId { get; init; }
        public int Score { get; init; }
    }

    public class MarkSubmissionCommand : ICommand<Guid>
    {
        public Guid TestId { get; init; }
        public List<AnswerScoreData> AnswerScoreDatas { get; init; }
    }

    public class MarkSubmissionCommandHandler : ICommandHandler<MarkSubmissionCommand, Guid>
    {
        private readonly ISubmissionRepository _submissionRepository;

        public MarkSubmissionCommandHandler(ISubmissionRepository submissionRepository)
        {
            _submissionRepository = submissionRepository;
        }

        public async Task<CommandResult<Guid>> Handle(MarkSubmissionCommand request, CancellationToken cancellationToken)
        {
            var submission = await _submissionRepository.FindOneAsync(request.TestId, cancellationToken);
            if (submission is null) 
                return CommandResult<Guid>.Error("The test of candidate does not exist.");

            foreach (var data in request.AnswerScoreDatas)
            {
                if (submission.Answers.Exists(x => x.Id == data.AnswerId)) 
                    return CommandResult<Guid>.Error("The answer does not exist in this candidate test.");

                submission.MarkTheAnswer(data.AnswerId, data.Score);
                await _submissionRepository.SaveAsync(submission, cancellationToken);
            }

            return CommandResult<Guid>.Success(submission.Id);
        }
    }
}
