using System.Collections;
using TestOnlineProject.Domain.Aggregates.TestAggregate;
using TestOnlineProject.Domain.SeedWork;

namespace TestOnlineProject.Domain.Aggregates.TestAggregate
{
    public class Test : AggregateRoot<Guid>
    {
        public string Title { get; private set; }
        public bool IsPublish { get; private set; }
        public DateTime ModifiedDate { get; private set; }
        public List<Question> Questions { get; private set; }

        private Test()
        {

        }
        
        public Test(string title)
        {
            Title = title;
            IsPublish = false;
            ModifiedDate = DateTime.Now;
            Questions = new();
        }

        public void UpdateTest(string title, bool isPublish)
        {
            Title = title;
            IsPublish = isPublish;
            ModifiedDate = DateTime.Now;
        }

        public void AddQuestion(Question request)
        {
            if (Questions is null) { Questions = new List<Question> { request }; }
            else Questions.Add(request);
            ModifiedDate = DateTime.Now;
        }

        public void UpdateQuestion(Question request)
        {
            var question = Questions.Find(x => x.Id == request.Id);
            question.UpdateQuestion(request.QuestionText, request.QuestionType, request.Point, request.TimeLimit);
            ModifiedDate = DateTime.Now;
        }

        public void RemoveQuestion(Guid questionId)
        {
            var question = Questions.Find(x => x.Id == questionId);
            Questions.Remove(question);
            ModifiedDate = DateTime.Now;
            if (Questions is null) { Questions = new(); }
        }
        public void AddChoice(Choice request)
        {
            var question = Questions.Find(x => x.Id == request.QuestionId);
            question.AddChoice(request);
            ModifiedDate = DateTime.Now;
        }

        public void UpdateChoice(Choice request)
        {
            var question = Questions.Find(x => x.Id == request.QuestionId);
            question.UpdateChoice(request);
            ModifiedDate = DateTime.Now;
        }

        public void RemoveChoice(Choice request)
        {
            var question = Questions.Find(x => x.Id == request.QuestionId);
            question.RemoveChoice(request);
            ModifiedDate = DateTime.Now;
        }
    }
}
