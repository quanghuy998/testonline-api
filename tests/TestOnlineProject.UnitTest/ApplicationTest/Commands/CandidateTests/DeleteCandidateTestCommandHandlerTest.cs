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
    public class DeleteCandidateTestCommandHandlerTest
    {
        private readonly Mock<ICandidateTestRepository> mockCandidateTestRepository = new();
        private readonly DeleteCandidateTestCommand command = new()
        {
            Id = Guid.NewGuid(),
        };

        [Fact]
        public async Task GivenCandidateTestInformation_WhenDeletingCandidateTest_ThenItShouldReturnSuccess()
        {
            var canidateTest = GivenSampleCandidateTest();
            mockCandidateTestRepository.Setup(p => p.FindOneAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(canidateTest);
            var handler = new DeleteCandidateTestCommandHandler(mockCandidateTestRepository.Object);

            var result = await handler.Handle(command, CancellationToken.None);

            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task GivenCandidateTestInformation_WhenDeletingCandidateTestDoesNotExist_ThenItShouldReturnErrorMessage()
        {
            mockCandidateTestRepository.Setup(p => p.FindOneAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(() => null);
            var handler = new DeleteCandidateTestCommandHandler(mockCandidateTestRepository.Object);

            var result = await handler.Handle(command, CancellationToken.None);

            string message = "The test of candidate does not exist.";
            Assert.Equal(message, result.Message);
        }

        private Submission GivenSampleCandidateTest()
        {
            return new Submission(Guid.NewGuid(), Guid.NewGuid(), new List<Answer>());
        }
    }
}
