using Moq;
using Xunit;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TestOnlineProject.API.Controllers;
using TestOnlineProject.Application.Queries.CandidateTests;
using TestOnlineProject.Domain.Aggregates.SubmissionAggregate;
using TestOnlineProject.Infrastructure.CQRS.Commands;
using TestOnlineProject.Infrastructure.CQRS.Queries;

namespace TestOnlineProject.UnitTest.ApiTest.CandidateTestControllerTest
{
    public class GetAllCandidateTestsActionTest
    {
        private readonly Mock<IQueryBus> mockQueryBus = new();
        private readonly Mock<ICommandBus> mockCommandBus = new();

        [Fact]
        public async Task GivenRequest_WhenGettingAllCandidateTests_ThenItShouldReturnValues()
        {
            var candidateTests = GivenSampleCandidateTest();
            mockQueryBus.Setup(p => p.SendAsync(It.IsAny<GetAllCandidateTestsQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(candidateTests);
            var controller = new CandidateTestController(mockQueryBus.Object, mockCommandBus.Object);

            var actionResult = await controller.GetAllCandidateTests(CancellationToken.None);
            var okResult = actionResult as OkObjectResult;

            Assert.Equal(200, okResult?.StatusCode);
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
