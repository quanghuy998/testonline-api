using Moq;
using Xunit;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using TestOnlineProject.Application.Queries.Candidates;
using TestOnlineProject.Domain.Aggregates.CandidateAggregate;
using TestOnlineProject.Domain.SeedWork;

namespace TestOnlineProject.UnitTest.ApplicationTest.Queries.Candidates
{
    public class GetAllCandidatesQueryHandlerTest
    {
        private readonly Mock<ICandidateRepository> mockCandidateRepository = new();
        private readonly GetAllCandidatesQuery query = new();

        [Fact]
        public async Task GivenRequest_WhenGettingAllCandidates_ThenItShouldReturnValues()
        {
            var candidates = GivenSampleCandidate();
            mockCandidateRepository.Setup(p => p.FindAllAsync(It.IsAny<BaseSpecification<Candidate>>(), It.IsAny<CancellationToken>())).ReturnsAsync(candidates);
            var handler = new GetAllCandidatesQueryHandler(mockCandidateRepository.Object);

            var result = await handler.Handle(query, CancellationToken.None);

            Assert.Equal(candidates, result);
        }

        private List<Candidate> GivenSampleCandidate()
        {
            return new List<Candidate>()
            {
                new Candidate("A", "Nguyen Van", "nguyenvana@gmail.com"),
                new Candidate("B", "Nguyen Thi", "nguyenthib@gmail.com"),
                new Candidate("C", "Huynh Thi", "huynhthic@gmail.com"),
            };
        }
    }
}
