using Moq;
using Xunit;
using System;
using System.Threading;
using System.Threading.Tasks;
using TestOnlineProject.Application.Commands.Tests;
using TestOnlineProject.Domain.Aggregates.TestAggregate;

namespace TestOnlineProject.UnitTest.ApplicationTest.Commands.Tests
{
    public class RemoveChoiceFromQuestionInTestHandlerTest
    {
        private Mock<ITestRepository> mockTestRepository = new();
        private RemoveChoiceFromQuestionInTestCommand command = new()
        {
            TestId = Guid.NewGuid(),
            QuestionId = Guid.NewGuid(),
            ChoiceId = Guid.NewGuid(),
        };

        [Fact]
        public async Task GivenAChoice_WhenRemovingChoiceFromQuestionInTest_ThenItShouldReturnSuccess()
        {
            var test = GivenSampleTest1();
            mockTestRepository.Setup(p => p.FindOneAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(test);
            var handler = new RemoveChoiceFromQuestionInTestCommandHandler(mockTestRepository.Object);

            var result = await handler.Handle(command, CancellationToken.None);

            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task GivenAChoice_WhenRemovingChoiceFromTheQuestionInTestDoesNotExist_ThenItShouldReturnErrorMessage()
        {
            mockTestRepository.Setup(p => p.FindOneAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(() => null);
            var handler = new RemoveChoiceFromQuestionInTestCommandHandler(mockTestRepository.Object);

            var result = await handler.Handle(command, CancellationToken.None);

            string message = "The test does not exist.";
            Assert.Equal(message, result.Message);
        }

        [Fact]
        public async Task GivenAChoice_WhenRemovingChoiceFromTheQuestionDoesNotExistInTest_ThenItShouldReturnErrorMessage()
        {
            var test = GivenSampleTest2();
            mockTestRepository.Setup(p => p.FindOneAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(test);
            var handler = new RemoveChoiceFromQuestionInTestCommandHandler(mockTestRepository.Object);

            var result = await handler.Handle(command, CancellationToken.None);

            string message = "The question does not exist in this test.";
            Assert.Equal(message, result.Message);
        }

        [Fact] 
        public async Task GivenInformationOfTheChoiceDoesNotExist_WhenRemovingChoiceFromTheQuestionInTest_ThenItShouldReturnErrorMesage()
        {
            var test = GivenSampleTest3();

            mockTestRepository.Setup(p => p.FindOneAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(test);
            var handler = new RemoveChoiceFromQuestionInTestCommandHandler(mockTestRepository.Object);

            var result = await handler.Handle(command, CancellationToken.None);

            string message = "The choice does not exist in this question.";
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

            string choiceText = "Choice number 1";
            bool isCorrect = true;
            var choice = new Choice(choiceText, isCorrect);
            choice.Id = command.ChoiceId;

            question.AddChoice(choice);
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

            string choiceText = "Choice number 1";
            bool isCorrect = true;
            var choice = new Choice(choiceText, isCorrect);
            choice.Id = command.ChoiceId;

            question.AddChoice(choice);
            test.AddQuestion(question);

            return test;
        }

        private Test GivenSampleTest3()
        {
            string title = "Test 1";
            var test = new Test(title);

            string questionText = "Question number 1?";
            int point = 4;
            var question = new Question(questionText, point, 30, QuestionType.MultipChoice);
            question.Id = command.QuestionId;

            string choiceText = "Choice number 1";
            bool isCorrect = true;
            var choice = new Choice(choiceText, isCorrect);
            choice.Id = Guid.NewGuid();

            question.AddChoice(choice);
            test.AddQuestion(question);

            return test;
        }
    }
}
