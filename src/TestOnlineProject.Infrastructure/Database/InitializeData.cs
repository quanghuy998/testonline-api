using TestOnlineProject.Domain.Aggregates.TestAggregate;

namespace TestOnlineProject.Infrastructure.Database
{
    public static class InitializeData
    {
        public static void CreateInitializeData(AppDbContext dbContext)
        {
        }


        public static Question CreateInitializeQuestion(AppDbContext dbContext)
        {
            var question = new Question("This is a question", 3, 30, QuestionType.MultipChoice);
            var choice = new Choice("Choice number 1", true);
            var choice2 = new Choice("Choice number 2", false);
            question.AddChoiceToQuestion(choice);
            question.AddChoiceToQuestion(choice2);

            return question;
        }

        public static Test CreateInitializeTest(AppDbContext dbContext)
        {
            var question = CreateInitializeQuestion(dbContext);
            var test = new Test("Test number 1");
            test.AddQuestion(question);

            return test;
        }
    }
}
