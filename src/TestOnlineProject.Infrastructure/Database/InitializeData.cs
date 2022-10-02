using TestOnlineProject.Domain.Aggregates.TestAggregate;

namespace TestOnlineProject.Infrastructure.Database
{
    public static class InitializeData
    {
        public static void CreateInitializeData(AppDbContext dbContext)
        {
            //dbContext.Tests.AddRange(CreateTest(dbContext));
            //dbContext.SaveChanges();
        }


        public static Test CreateTest(AppDbContext dbContext)
        {
            var question = new Question("This is a question", 3, 30, QuestionType.MultipChoice);
            question.Id = Guid.NewGuid();

            var test = new Test("Test number 1");
            test.AddQuestion(question);

            return test;
        }
    }
}
