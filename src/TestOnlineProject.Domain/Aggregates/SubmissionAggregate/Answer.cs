using TestOnlineProject.Domain.SeedWork;

namespace TestOnlineProject.Domain.Aggregates.SubmissionAggregate
{
    public class Answer : Entity<Guid>
    {
        public string QuestionText { get; private set; }
        public string AnswerText { get; private set; }
        public int Score { get; private set; }

        private Answer()
        {

        }

        public Answer(string questionText, string answerText)
        {
            QuestionText = questionText;
            AnswerText = answerText;
            Score = 0;
        }

        public void SetResult(int score)
        {
            Score = score;
        }
    }
}
