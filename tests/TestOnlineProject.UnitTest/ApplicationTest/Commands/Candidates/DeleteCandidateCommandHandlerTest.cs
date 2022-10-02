using Moq;
using Xunit;
using System;
using System.Threading;
using System.Threading.Tasks;
using TestOnlineProject.Application.Commands.Candidates;
using TestOnlineProject.Domain.Aggregates.CandidateAggregate;

namespace TestOnlineProject.UnitTest.ApplicationTest.Commands.Candidates
{
    public class DeleteCandidateCommandHandlerTest
    {
        private readonly Mock<ICandidateRepository> mockCandidateRepository = new();
        private readonly DeleteCandidateCommand command = new() { Id = Guid.NewGuid() };

        [Fact]
        public async Task GivenAnCandidateInformation_WhenDeletingCandidate_ThenItShouldReturnSuccess()
        {
            var candidate = GivenSampleCandidate();
            mockCandidateRepository.Setup(p => p.FindOneAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(candidate);
            var handler = new DeleteCandidateCommandHandler(mockCandidateRepository.Object);

            var result = await handler.Handle(command, CancellationToken.None);

            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task GivenAnCandidateInformation_WhenDeletingCandidateDoesNotExist_ThenItShouldReturnErrorMessage()
        {
            mockCandidateRepository.Setup(p => p.FindOneAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(() => null);
            var handler = new DeleteCandidateCommandHandler(mockCandidateRepository.Object);

            var result = await handler.Handle(command, CancellationToken.None);

            string message = "The candidate does not exist.";
            Assert.Equal(message, result.Message);
        }

        private Candidate GivenSampleCandidate()
        {
            return new Candidate("Musso", "Guillaume", "guillaumemusoo@gmail.com");
        }
    }
}
