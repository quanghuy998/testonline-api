using TestOnlineProject.Domain.SeedWork;

namespace TestOnlineProject.Domain.Aggregates.SubmissionAggregate
{
    public class Answer : Entity<Guid>
    {
        public Guid QuestionId { get; }
        public string Answer { get; private set; }
        public string Question { get; private set; }
        public int Score { get; private set; }

        private Answer()
        {

        }

        public Answer(Guid questionId)
        {
            QuestionId = questionId;
            Answer = null;
            Score = 0;
        }

        public void AddAnswerText(string answerText)
        {
            Answer = answerText;
        }

        public void SetResult(int score)
        {
            Score = score;
        }
    }
}
