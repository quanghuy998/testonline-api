using Moq;
using Xunit;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TestOnlineProject.Application.Queries.CandidateTests;
using TestOnlineProject.Domain.Aggregates.SubmissionAggregate;
using TestOnlineProject.Domain.SeedWork;

namespace TestOnlineProject.UnitTest.ApplicationTest.Queries.CandidateTests
{
    public class GetAllCandidatesQueryHandlerTest
    {
        private readonly Mock<ICandidateTestRepository> mockCandidateTestRepository = new();
        private readonly GetAllCandidateTestsQuery query = new();

        [Fact]
        public async Task GivenRequest_WhenGettingAllCandidateTests_ThenItShouldReturnValues()
        {
            var candidateTests = GivenSampleCandidateTest();
            mockCandidateTestRepository.Setup(p => p.FindAllAsync(It.IsAny<BaseSpecification<Submission>>(), It.IsAny<CancellationToken>())).ReturnsAsync(candidateTests);
            var handler = new GetAllCandidateTestsQueryHandler(mockCandidateTestRepository.Object);

            var result = await handler.Handle(query, CancellationToken.None);

            Assert.Equal(candidateTests, result);
        }

        private List<Submission> GivenSampleCandidateTest()
        {
            return new List<Submission>()
            {
                new Submission(Guid.NewGuid(), Guid.NewGuid(), new List<Answer>()),
                new Submission(Guid.NewGuid(), Guid.NewGuid(), new List<Answer>()),
                new Submission(Guid.NewGuid(), Guid.NewGuid(), new List<Answer>()),
            };
        }
    }
}
