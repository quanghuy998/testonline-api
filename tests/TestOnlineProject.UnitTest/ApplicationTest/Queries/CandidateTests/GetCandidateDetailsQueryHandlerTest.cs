using Moq;
using Xunit;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using TestOnlineProject.Application.Queries.CandidateTests;
using TestOnlineProject.Domain.Aggregates.SubmissionAggregate;


namespace TestOnlineProject.UnitTest.ApplicationTest.Queries.CandidateTests 
{
    public class GetCandidateDetailsQueryHandlerTest
    {
        private readonly Mock<ICandidateTestRepository> mockCandidateTestRepository = new();
        private readonly GetCandidateTestDetailsQuery query = new() { Id = Guid.NewGuid() };

        [Fact]
        public async Task GivenRequest_WhenGettingCandidateTestDetails_ThenItShouldReturnAnCandidateTest()
        {
            var candidateTest = GivenSampleCandidateTest();
            mockCandidateTestRepository.Setup(p => p.FindOneAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(candidateTest);
            var handler = new GetCandidateTestDetailsQueryHandler(mockCandidateTestRepository.Object);

            var result = await handler.Handle(query, CancellationToken.None);

            Assert.Equal(candidateTest, result);
        }

        [Fact]
        public async Task GivenRequest_WhenGettingCandidateTestDetailsOfTheCandidateTestDoesNotExist_ThenItShouldReturnNull()
        {
            mockCandidateTestRepository.Setup(p => p.FindOneAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(() => null);
            var handler = new GetCandidateTestDetailsQueryHandler(mockCandidateTestRepository.Object);

            var result = await handler.Handle(query, CancellationToken.None);

            Assert.Null(result);
        }

        private Submission GivenSampleCandidateTest()
        {
            return new Submission(Guid.NewGuid(), Guid.NewGuid(), new List<Answer>());
        }
    }
}
