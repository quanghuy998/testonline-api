using Moq;
using Xunit;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using TestOnlineProject.Application.Commands.CandidateTests;
using TestOnlineProject.Domain.Aggregates.SubmissionAggregate;

namespace TestOnlineProject.UnitTest.ApplicationTest.Commands.CandidateTests
{
    public class MarkTheAnswerInCandidateTestCommandHandlerTest
    {
        private readonly Mock<ICandidateTestRepository> mockCandidateTestRepository = new();
        private readonly MarkTheAnswerInCandidateTestCommand command = new()
        {
            AnswerId = Guid.NewGuid(),
            CandidateTestId = Guid.NewGuid(),
            Score = 10,
        };

        [Fact]
        public async Task GivenAnScore_WhenMarkingTheAnswerInCandidateTest_ThenItShouldBeReturnSuccess()
        {
            var candidateTest = GivenSampleCandidateTest();
            mockCandidateTestRepository.Setup(p => p.FindOneAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(candidateTest);
            var handler = new MarkTheAnswerInCandidateTestCommandHandler(mockCandidateTestRepository.Object);

            var result = await handler.Handle(command, CancellationToken.None);

            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task GivenAnScore_WhenMarkingTheAnswerInCandidateTestDoesNotExist_ThenItShouldReturnErrorMessage()
        {
            mockCandidateTestRepository.Setup(p => p.FindOneAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(() => null);
            var handler = new MarkTheAnswerInCandidateTestCommandHandler(mockCandidateTestRepository.Object);

            var result = await handler.Handle(command, CancellationToken.None);

            string message = "The test of candidate does not exist.";
            Assert.Equal(message, result.Message);
        }

        [Fact]
        public async Task GivenAnScore_WhenMarkingTheAnswerDoesNotExistInCandidateTest_ThenItShouldReturnErrorMessage()
        {
            var candidateTest = GivenSampleCandidateTestWithDifferentAnswerId();
            mockCandidateTestRepository.Setup(p => p.FindOneAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(candidateTest);
            var handler = new MarkTheAnswerInCandidateTestCommandHandler(mockCandidateTestRepository.Object);

            var result = await handler.Handle(command, CancellationToken.None);
            string message = "The answer does not exist in this candidate test.";
            Assert.Equal(message, result.Message);
        }

        private Submission GivenSampleCandidateTest()
        {
            var answer = new Answer(Guid.NewGuid());
            answer.Id = command.AnswerId;
            var answers = new List<Answer>() { answer };

            return new Submission(Guid.NewGuid(), Guid.NewGuid(), answers);
        }

        private Submission GivenSampleCandidateTestWithDifferentAnswerId()
        {
            var answer = new Answer(Guid.NewGuid());
            answer.Id = Guid.NewGuid();
            var answers = new List<Answer>() { answer };

            return new Submission(Guid.NewGuid(), Guid.NewGuid(), answers);
        }
    }
}
