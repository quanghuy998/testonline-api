namespace TestOnlineProject.API.Dtos
{
    public class CandateTestRequest
    {
        public record CreateCandidateTestRequest(Guid testId, Guid candidateId);
        public record MarkTheAnswerInCandidateTestRequest(Guid answerId, int score);
        public record SubmitAnswerRequest(List<SubmitAnswerDto> answers);
    }

    public class SubmitAnswerDto
    {
        public Guid AnswerId { get; init; }
        public string AnswerText { get; init; }
    }
}
