using Moq;
using Xunit;
using System;
using System.Threading;
using System.Threading.Tasks;
using TestOnlineProject.Application.Commands.Tests;
using TestOnlineProject.Domain.Aggregates.TestAggregate;

namespace TestOnlineProject.UnitTest.ApplicationTest.Commands.Tests
{
    public class AddQuestionToTestHandlerTest
    {
        private Mock<ITestRepository> mockTestRepository = new();
        private AddQuestionToTestCommand command = new()
        {
            TestId = Guid.NewGuid(),
            QuestionText = "Question Text",
            TimeLimit = 30,
            Point = 10,
            QuestionType = QuestionType.Code
        };

        [Fact]
        public async Task GivenAnQuestion_WhenAddingQuestionToTest_ThenItShouldReturnSuccess()
        {
            var test = GivenSampleTest();
            var question = GivenSampleQuestion();
            mockTestRepository.Setup(p => p.FindOneAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(test);
            var handler = new AddQuestionToTestCommandHandler(mockTestRepository.Object);

            var result = await handler.Handle(command, CancellationToken.None);

            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task GivenAnQuestion_WhenAddingQuestionToTheTestDoesNotExist_ThenItShouldReturnErrorMessage()
        {
            var question = GivenSampleQuestion();
            mockTestRepository.Setup(p => p.FindOneAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(() => null);
            var handler = new AddQuestionToTestCommandHandler(mockTestRepository.Object);

            var result = await handler.Handle(command, CancellationToken.None);

            string message = "The test does not exist.";
            Assert.Equal(message, result.Message);
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
