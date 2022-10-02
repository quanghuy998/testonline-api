using Moq;
using Xunit;
using System;
using System.Threading;
using System.Threading.Tasks;
using TestOnlineProject.Application.Commands.CandidateTests;
using TestOnlineProject.Domain.Aggregates.SubmissionAggregate;
using TestOnlineProject.Domain.Aggregates.TestAggregate;

namespace TestOnlineProject.UnitTest.ApplicationTest.Commands.CandidateTests
{
    public class CreateCandidateTestCommandHandlerTest
    {
        private readonly Mock<ICandidateTestRepository> mockCandidateTestRepository = new();
        private readonly Mock<ITestRepository> mockTestRepository = new();
        private readonly CreateCandidateTestCommand command = new()
        {
            TestId = Guid.NewGuid(),
            CandidateId = Guid.NewGuid(),
        };

        [Fact]
        public async Task GivenInformation_WhenCreatingCandidateTest_ThenItShouldReturnSucess()
        {
            var test = GivenSampleTest();
            mockTestRepository.Setup(p => p.FindOneAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(test);
            var handler = new CreateCandidateTestCommandHandler(mockCandidateTestRepository.Object, mockTestRepository.Object);

            var result = await handler.Handle(command, CancellationToken.None);

            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task GivenInformation_WhenCreatingCandidateTestDoesNotExist_ThenItShouldReturnErrorMessage()
        {
            mockTestRepository.Setup(p => p.FindOneAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(() => null);
            var handler = new CreateCandidateTestCommandHandler(mockCandidateTestRepository.Object, mockTestRepository.Object);

            var result = await handler.Handle(command, CancellationToken.None);

            string message = "Can not create a candidate test. Because the test does not exist.";
            Assert.Equal(message, result.Message);
        }

        [Fact]
        public async Task GivenInformation_WhenCreatingCandidateTestDoesNotExistAnyQuestion_ThenItShouldReturnErrorMessage()
        {
            var test = GivenSampleTestWithNoQuestion();
            mockTestRepository.Setup(p => p.FindOneAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(test);
            var handler = new CreateCandidateTestCommandHandler(mockCandidateTestRepository.Object, mockTestRepository.Object);

            var result = await handler.Handle(command, CancellationToken.None);

            string message = "Can not create a candidate test. Because the test does not exist any question.";
            Assert.Equal(message, result.Message);
        }

        private Test GivenSampleTest()
        {
            var test = new Test("Test 1");
            var question = new Question("Question number 1?", 4, 30, QuestionType.MultipChoice);
            question.Id = Guid.NewGuid();
            test.AddQuestion(question);

            return test;
        }

        private Test GivenSampleTestWithNoQuestion()
        {
            return new Test("Test 2");
        }
    }
}
