using Moq;
using Xunit;
using System;
using System.Threading;
using System.Threading.Tasks;
using TestOnlineProject.Application.Queries.Candidates;
using TestOnlineProject.Domain.Aggregates.CandidateAggregate;

namespace TestOnlineProject.UnitTest.ApplicationTest.Queries.Candidates
{
    public class GetCandidateDetailsQueryHandlerTest
    {
        private readonly Mock<ICandidateRepository> mockCandidateRepository = new();
        private readonly GetCandidateDetailsQuery query = new() { Id = Guid.NewGuid() };

        [Fact]
        public async Task GivenRequest_WhenGettingCandidateDetails_ThenItShouldReturnAnCandidate()
        {
            var candidate = GivenSampleCandidate();
            mockCandidateRepository.Setup(p => p.FindOneAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(candidate);
            var handler = new GetCandidateDetailsQueryHandler(mockCandidateRepository.Object);

            var result = await handler.Handle(query, CancellationToken.None);

            Assert.Equal(candidate, result);
        }

        [Fact]
        public async Task GivenRequest_WhenGettingCandidateDetailsOfTheCandidateDoesNotExist_ThenItShouldReturnNull()
        {
            mockCandidateRepository.Setup(p => p.FindOneAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(() => null);
            var handler = new GetCandidateDetailsQueryHandler(mockCandidateRepository.Object);

            var result = await handler.Handle(query, CancellationToken.None);

            Assert.Null(result);
        }

        private Candidate GivenSampleCandidate()
        {
            return new Candidate("Huy", "Huynh Duc Quang", "huyhuynhducquang@gmail.com");
        }
    }
}
