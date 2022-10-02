using Moq;
using Xunit;
using System;
using System.Threading;
using System.Threading.Tasks;
using TestOnlineProject.Application.Commands.Tests;
using TestOnlineProject.Domain.Aggregates.TestAggregate;

namespace TestOnlineProject.UnitTest.ApplicationTest.Commands.Tests
{
    public class UpdateQuestionInTestHandlerTest
    {
        private Mock<ITestRepository> mockTestRepository = new();

        private UpdateQuestionInTestCommand command = new()
        {
            TestId = Guid.NewGuid(),
            QuestionId = Guid.NewGuid(),
            QuestionText = "Question Text",
            TimeLimit = 30,
            Point = 10,
            QuestionType = QuestionType.Code
        };

        [Fact]
        public async Task GivenInformation_WhenUpdatingQuestionInTest_ThenItShouldBeUpdate()
        {
            var test = GivenSampleTest1();
            mockTestRepository.Setup(p => p.FindOneAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(test);
            var handler = new UpdateQuestionInTestCommandHandler(mockTestRepository.Object);

            var result = await handler.Handle(command, CancellationToken.None);

            Assert.True(result.IsSuccess);
            Assert.Equal(command.QuestionText, test.Questions[0].QuestionText);
            Assert.Equal(command.Point, test.Questions[0].Point);
            Assert.Equal(command.TimeLimit, test.Questions[0].TimeLimit);
            Assert.Equal(command.QuestionType, test.Questions[0].QuestionType);
        }

        [Fact]
        public async Task GivenInformation_WhenUpdatingQuestionInTestDoesNotExist_ThenItShouldReturnErrorMessage()
        {
            mockTestRepository.Setup(p => p.FindOneAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(() => null);
            var handler = new UpdateQuestionInTestCommandHandler(mockTestRepository.Object);

            var result = await handler.Handle(command, CancellationToken.None);

            string message = "The test does not exist.";
            Assert.Equal(message, result.Message);
        }

        [Fact]
        public async Task GivenInformation_WhenUpdatingQuestionDoesNotExistInTest_ThenItShouldReturnErrorMessage()
        {
            var test = GivenSampleTest2();
            mockTestRepository.Setup(p => p.FindOneAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(test);
            var handler = new UpdateQuestionInTestCommandHandler(mockTestRepository.Object);

            var result = await handler.Handle(command, CancellationToken.None);

            string message = "The question does not exist in this test.";
            Assert.Equal(message, result.Message);
        }

        private Test GivenSampleTest1()
        {
            string title = "Test 1";
            var test = new Test(title);

            string questionText = "Question number 1?";
            int point = 4;
            var question = new Question(questionText, point, 30, QuestionType.MultipChoice);
            question.Id = command.QuestionId;

            test.AddQuestion(question);

            return test;
        }

        private Test GivenSampleTest2()
        {
            string title = "Test 1";
            var test = new Test(title);

            string questionText = "Question number 1?";
            int point = 4;
            var question = new Question(questionText, point, 30, QuestionType.MultipChoice);
            question.Id = Guid.NewGuid();

            test.AddQuestion(question);

            return test;
        }
    }
}
