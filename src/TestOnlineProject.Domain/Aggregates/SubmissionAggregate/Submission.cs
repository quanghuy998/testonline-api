using TestOnlineProject.Domain.SeedWork;

namespace TestOnlineProject.Domain.Aggregates.SubmissionAggregate
{
    public class Submission : AggregateRoot<Guid>
    {
        public Guid TestId { get; }
        public Guid CandidateId { get; }
        public Guid Axaminer { get; }
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
            FinishedDate = DateTime.Now;
        }

        public void MarkTheAnswer(Guid answerId, int result)
        {
            var answer = Answers.Find(x => x.Id == answerId);
            answer.SetResult(result);
        }
    }
}
