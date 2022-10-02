using TestOnlineProject.Domain.SeedWork;

namespace TestOnlineProject.Domain.Aggregates.SubmissionAggregate
{
    public class Submission : AggregateRoot<Guid>
    {
        public Guid TestId { get; }
        public Guid CandidateId { get; }
        public List<Answer> Answers { get; private set; }
        public DateTime FinishedDate { get; private set; }

        private Submission()
        {

        }

        public Submission(Guid testID, Guid candidateId, List<Answer> answers)
        {
            TestId = testID;
            CandidateId = candidateId;
            Answers = answers;
            FinishedDate = default(DateTime);
        }

        public void AddAnswers(List<AnswerData> answerDatas)
        {
            foreach(var answerdata in answerDatas)
            {
                var answer = Answers.Find(x => x.Id == answerdata.AnswerId);
                answer.AddAnswerText(answerdata.AnswerText);
            }
            FinishedDate = DateTime.Now;
        }

        public void MarkTheAnswerInCandidateTest(Guid answerId, int result)
        {
            var answer = Answers.Find(x => x.Id == answerId);
            answer.SetResult(result);
        }
    }
}
