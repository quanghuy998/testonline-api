using Moq;
using Xunit;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TestOnlineProject.API.Controllers;
using TestOnlineProject.Infrastructure.CQRS.Commands;
using TestOnlineProject.Infrastructure.CQRS.Queries;
using TestOnlineProject.Application.Queries.CandidateTests;
using TestOnlineProject.Domain.Aggregates.SubmissionAggregate;

namespace TestOnlineProject.UnitTest.ApiTest.CandidateTestControllerTest
{
    public class GetCandidateTestDetailsActionTest
    {
        private readonly Mock<IQueryBus> mockQueryBus = new();
        private readonly Mock<ICommandBus> mockCommandBus = new();

        [Fact]
        public async Task GivenRequest_WhenGettingCandidateTestDetails_ThenItShouldReturnValues()
        {
            var id = Guid.NewGuid();
            var candidateTest = GivenSampleCandidateTest();
            mockQueryBus.Setup(p => p.SendAsync(It.IsAny<GetCandidateTestDetailsQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(candidateTest);
            var controller = new CandidateTestController(mockQueryBus.Object, mockCommandBus.Object);

            var actionResult = await controller.GetCandidateTestDetails(id, CancellationToken.None);
            var okResult = actionResult as OkObjectResult;

            Assert.Equal(200, okResult?.StatusCode);
        }

        private Submission GivenSampleCandidateTest()
        {
            return new Submission(Guid.NewGuid(), Guid.NewGuid(), new List<Answer>());
        }
    }
}
