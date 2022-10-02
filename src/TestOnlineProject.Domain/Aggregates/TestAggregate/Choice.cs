using TestOnlineProject.Domain.SeedWork;

namespace TestOnlineProject.Domain.Aggregates.TestAggregate
{
    public class Choice : Entity<Guid>
    {
        public Guid QuestionId { get; private set; }
        public string ChoiceText { get; private set; }
        public bool IsCorrect { get; private set; }

        private Choice()
        {

        }

        public Choice(Guid questionId, string choiceText, bool isCorrect)
        {
            QuestionId = questionId;
            ChoiceText = choiceText;
            IsCorrect = isCorrect;
        }

        public void UpdateChoice(string choiceText, bool isCorrect)
        {
            ChoiceText = choiceText;
            IsCorrect = isCorrect;
        }
    }
}
