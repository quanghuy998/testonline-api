using Moq;
using Xunit;
using System;
using System.Threading;
using System.Threading.Tasks;
using TestOnlineProject.Application.Commands.Tests;
using TestOnlineProject.Domain.Aggregates.TestAggregate;

namespace TestOnlineProject.UnitTest.ApplicationTest.Commands.Tests
{
    public class RemoveQuestionFromTestHandlerTest
    {
        private Mock<ITestRepository> mockTestRepository = new();
        private RemoveQuestionFromTestCommand command = new()
        {
            TestId = Guid.NewGuid(),
            QuestionId = Guid.NewGuid()
        };

        [Fact]
        public async Task GivenAnQuestion_WhenRemovingQuestionFromTest_ThenItShoudReturnSuccess()
        {
            var test = GivenSampleTest();
            var question = GivenSampleQuestion();
            question.Id = command.QuestionId;
            test.AddQuestion(question);
            mockTestRepository.Setup(p => p.FindOneAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(test);
            var handler = new RemoveQuestionFromTestCommandHandler(mockTestRepository.Object);

            var result = await handler.Handle(command, CancellationToken.None);

            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task GivenAnQuestion_WhenRemovingQuestionFromTheTestDoesNotExist_ThenItShouldBeReturnErrorMessage()
        {
            mockTestRepository.Setup(p => p.FindOneAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(() => null);
            var handler = new RemoveQuestionFromTestCommandHandler(mockTestRepository.Object);

            var result = await handler.Handle(command, CancellationToken.None);

            string message = "The test does not exist.";
            Assert.Equal(message, result.Message);
        }

        [Fact]
        public async Task GivenInformationOfTheTestDoesNotExist_WhenRemovingQuestionFromTest_ThenItShoudBeReturnErrorMessage()
        {
            var test = GivenSampleTest();
            var question = GivenSampleQuestion();
            question.Id = Guid.NewGuid();
            test.AddQuestion(question);
            mockTestRepository.Setup(p => p.FindOneAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(test);
            var handler = new RemoveQuestionFromTestCommandHandler(mockTestRepository.Object);

            var result = await handler.Handle(command, CancellationToken.None);

            string message = "The question does not exist in this test.";
            Assert.Equal(message, result.Message);
        }

        private Test GivenSampleTest()
        {
            string title = "Test 1";
            return new Test(title);
        }

        private Question GivenSampleQuestion()
        {
            string questionText = "Question number 1?";
            int point = 4;
            int timeLimit = 30;

            return new Question(questionText, point, timeLimit, QuestionType.MultipChoice);
        }
    }
}
