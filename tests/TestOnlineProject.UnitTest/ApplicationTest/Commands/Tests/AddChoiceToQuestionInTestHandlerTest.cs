using Moq;
using Xunit;
using System;
using System.Threading;
using System.Threading.Tasks;
using TestOnlineProject.Application.Commands.Tests;
using TestOnlineProject.Domain.Aggregates.TestAggregate;

namespace TestOnlineProject.UnitTest.ApplicationTest.Commands.Tests
{
    public class AddChoiceToQuestionInTestHandlerTest
    {
        private Mock<ITestRepository> mockTestRepository = new();
        private AddChoiceToQuestionInTestCommand command = new()
        {
            TestId = Guid.NewGuid(),
            QuestionId = Guid.NewGuid(),
            ChoiceText = "This is a choice text",
            IsCorrect = true
        };

        [Fact]
        public async Task GivenAChoice_WhenAddingChoiceToQuestionInTest_ThenItShouldReturnSuccess()
        {
            var test = GivenSampleTest1();
            mockTestRepository.Setup(p => p.FindOneAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(test);
            var handler = new AddChoiceToQuestionInTestCommandHandler(mockTestRepository.Object);

            var result = await handler.Handle(command, CancellationToken.None);

            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task GivenAChoice_WhenAddingChoiceToTheQuestionInTestDoesNotExist_ThenItShouldReturnErrorMessage()
        {
            mockTestRepository.Setup(p => p.FindOneAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(() => null);
            var handler = new AddChoiceToQuestionInTestCommandHandler(mockTestRepository.Object);

            var result = await handler.Handle(command, CancellationToken.None);

            string message = "The test does not exist.";
            Assert.Equal(message, result.Message);
        }

        [Fact]
        public async Task GivenAChoice_WhenAddingChoiceToTheQuestionDoesNotExistInTest_ThenItShouldReturnErrorMessage()
        {
            var test = GivenSampleTest2();
            mockTestRepository.Setup(p => p.FindOneAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(test);
            var handler = new AddChoiceToQuestionInTestCommandHandler(mockTestRepository.Object);

            var result = await handler.Handle(command, CancellationToken.None);

            string message = "The question does not exist in this test.";
            Assert.Equal(message, result.Message);
        }

        [Fact]
        public async Task GivenAChoice_WhenAddingChoiceToTheQuestionThatTypeDoesNotMultipChoiceInTest_ThenItShouldReturnErrorMessages()
        {
            var test = GivenSampleTest3();
            mockTestRepository.Setup(p => p.FindOneAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(test);
            var handler = new AddChoiceToQuestionInTestCommandHandler(mockTestRepository.Object);

            var result = await handler.Handle(command, CancellationToken.None);

            string message = "The question type is not multip choice.";
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
            string title = "Test 2";
            var test = new Test(title);

            string questionText = "Question number 1?";
            int point = 4;
            var question = new Question(questionText, point, 30, QuestionType.MultipChoice);
            question.Id = Guid.NewGuid();

            test.AddQuestion(question);

            return test;
        }

        private Test GivenSampleTest3()
        {
            string title = "Test 3";
            var test = new Test(title);

            string questionText = "Question number 1?";
            int point = 4;
            var question = new Question(questionText, point, 30, QuestionType.Code);
            question.Id = command.QuestionId;

            test.AddQuestion(question);

            return test;
        }
    }
}
