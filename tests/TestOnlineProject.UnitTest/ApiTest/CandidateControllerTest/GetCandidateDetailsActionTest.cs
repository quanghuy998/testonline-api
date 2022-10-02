using Moq;
using Xunit;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestOnlineProject.API.Controllers;
using TestOnlineProject.Application.Queries.Candidates;
using TestOnlineProject.Domain.Aggregates.CandidateAggregate;
using TestOnlineProject.Infrastructure.CQRS.Commands;
using TestOnlineProject.Infrastructure.CQRS.Queries;

namespace TestOnlineProject.UnitTest.ApiTest.CandidateControllerTest
{
    public class GetCandidateDetailsActionTest
    {
        private readonly Mock<IQueryBus> mockQueryBus = new();
        private readonly Mock<ICommandBus> mockCommandBus = new();
        
        [Fact]
        public async Task GivenRequest_WhenGettingCandidateDetails_ThenItShouldReturnValues()
        {
            var id = Guid.NewGuid();
            var candidate = GivenCandidateSample();
            mockQueryBus.Setup(p => p.SendAsync(It.IsAny<GetCandidateDetailsQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(candidate);
            var controller = new CandidateController(mockQueryBus.Object, mockCommandBus.Object);

            var actionResult = await controller.GetCandidateDetails(id, CancellationToken.None);
            var okResult = actionResult as OkObjectResult;

            Assert.Equal(200, okResult?.StatusCode);
        }

        private Candidate GivenCandidateSample()
        {
            return new Candidate("Huy", "Huynh Duc Quang", "huy@gmail.com");
        }
    }
}
