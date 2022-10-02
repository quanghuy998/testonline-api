using Xunit; 
using TestOnlineProject.Domain.Aggregates.TestAggregate;

namespace TestOnlineProject.UnitTest.DomainTest
{
    public class TestAggregateTest
    {
        [Fact]
        public void GivenInformation_WhenCreatingTest_ThenItShouldBeCreated()
        {
            string title = "Test 1";

            var test = new Test(title);

            Assert.Equal(title, test.Title);
            Assert.False(test.IsPublish);
            Assert.Empty(test.Questions);
        }

        [Fact]
        public void GivenInformation_WhenUpdatingTest_ThenItShouldBeUpdated()
        {
            string title = "Test 2";
            var test = GivenSampleTest();

            test.UpdateTest(title);

            Assert.Equal(title, test.Title);
        }

        [Fact]
        public void GivenPublicRequest_WhenPublicizingTest_ThenItShouldBePublicized()
        {
            bool isPublic = true;
            var test = GivenSampleTest();

            test.PublicAndNotPublicTest(isPublic);

            Assert.True(test.IsPublish);
        }

        [Fact]
        public void GivenAnQuestion_WhenAddingQuestionToTest_ThenItShouldBeAdded()
        {
            var question = GivenSampleQuestion();
            var test = GivenSampleTest();

            test.AddQuestion(question);

            Assert.Contains(question, test.Questions);
        }

        [Fact]
        public void GivenInformation_WhenUpdatingQuestionInTest_ThenItShouldBeUpdated()
        {
            string questionText = "Updated";
            int point = 30;
            int timeLitmit = 10;
            QuestionType questionType = QuestionType.Code;
            var question = GivenSampleQuestion();
            var test = GivenSampleTest();
            test.AddQuestion(question);
            question.UpdateQuestion(questionText, questionType, point, timeLitmit);

            test.UpdateQuestion(question);

            Assert.Contains(question, test.Questions);
            Assert.Equal(questionText, test.Questions[0].QuestionText);
            Assert.Equal(point, test.Questions[0].Point);
            Assert.Equal(timeLitmit, test.Questions[0].TimeLimit);
            Assert.Equal(questionType, test.Questions[0].QuestionType);
        }

        [Fact]
        public void GivenAnQuestion_WhenRemovingAnQuestionFromTest_ThenItShouldBeRemoved()
        {
            var question = GivenSampleQuestion();
            var test = GivenSampleTest();

            test.AddQuestion(question);
            test.RemoveQuestion(question.Id);

            Assert.Empty(test.Questions);
        }

        private Question GivenSampleQuestion()
        {
            string questionText = "Question number 1?";
            int point = 4;
            int timeLimit = 30;
            return new Question(questionText, point, timeLimit, QuestionType.MultipChoice);
        }

        private Test GivenSampleTest()
        {
            string title = "Test 1";
            return new Test(title);
        }
    }
}
