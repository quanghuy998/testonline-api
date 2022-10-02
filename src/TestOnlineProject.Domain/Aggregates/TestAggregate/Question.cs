using TestOnlineProject.Domain.SeedWork;

namespace TestOnlineProject.Domain.Aggregates.TestAggregate
{
    public class Question : Entity<Guid>
    {
        public string QuestionText { get; private set; }
        public int Point { get; private set; }
        public int TimeLimit { get; private set; }
        public QuestionType QuestionType { get; private set; }
        public List<Choice> Choices { get; private set; }

        private Question()
        {

        }

        public Question(string questionTest, int point, int timeLitmit, QuestionType questionType)
        {
            QuestionText = questionTest;
            Point = point;
            TimeLimit = timeLitmit;
            QuestionType = questionType;
            Choices = new();
        }

        public void UpdateQuestion(string questionText, QuestionType questionType, int point, int timeLitmit)
        {
            QuestionText = questionText;
            QuestionType = questionType;
            Point = point;
            TimeLimit = timeLitmit;
        }

        public void AddChoice(Choice request)
        {
            if (Choices is null) { Choices = new List<Choice> { request }; }
            else Choices.Add(request);
        }

        public void UpdateChoice(Choice request)
        {
            var choice = Choices.Find(x => x.Id == request.Id);
            choice.UpdateChoice(request.ChoiceText, request.IsCorrect);
        }

        public void RemoveChoice(Choice request)
        {
            var choice = Choices.Find(x => x.Id == request.Id);
            Choices.Remove(choice);
        }
    }
}
