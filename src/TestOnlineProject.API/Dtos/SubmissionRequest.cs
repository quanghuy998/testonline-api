using TestOnlineProject.Application.Commands.Submissions;

namespace TestOnlineProject.API.Dtos
{
    public class SubmissionRequest
    {
        public record CreateSubmissionRequest(Guid testId, Guid candidateId);
        public record MarkSubmissionRequest(Guid answerId, List<AnswerScoreData> answerScoreDatas);
    }
}
