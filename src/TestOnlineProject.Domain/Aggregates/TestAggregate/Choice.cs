using TestOnlineProject.Domain.SeedWork;

namespace TestOnlineProject.Domain.Aggregates.TestAggregate
{
    public class Choice : Entity<Guid>
    {
        public string ChoiceText { get; private set; }
        public bool IsCorrect { get; private set; }

        private Choice()
        {

        }

        public Choice(string choiceText, bool isCorrect)
        {
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
