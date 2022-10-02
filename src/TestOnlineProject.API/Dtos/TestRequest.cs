using TestOnlineProject.Domain.Aggregates.TestAggregate;

namespace TestOnlineProject.API.Dtos
{
    public class TestRequest
    {
        public record CreateTestRequest(string title);
        public record UpdateTestRequest(string title);
        public record PublicAndNotPublicTestRequest(bool isPublic);
        public record AddQuestionToTestRequest(string questionText, int timeLimit, int point, QuestionType questionType);
        public record UpdateQuestionInTestRequest(string questionText, int timeLimit, int point, QuestionType questionType);
        public record AddChoiceToQuestionInTestRequest(string choiceText, bool isCorrect);
        public record UpdateChoiceInQuestionInTestRequest(string choiceText, bool isCorrect);
    }
}
