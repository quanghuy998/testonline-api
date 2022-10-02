using Moq;
using Xunit;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TestOnlineProject.API.Controllers;
using TestOnlineProject.Application.Queries.Candidates;
using TestOnlineProject.Domain.Aggregates.CandidateAggregate;
using TestOnlineProject.Infrastructure.CQRS.Commands;
using TestOnlineProject.Infrastructure.CQRS.Queries;

namespace TestOnlineProject.UnitTest.ApiTest.CandidateControllerTest
{
    public class GetAllCandidatesActionTest
    {
        private readonly Mock<IQueryBus> mockQueryBus = new();
        private readonly Mock<ICommandBus> mockCommandBus = new();

        [Fact]
        public async Task GivenRequest_WhenGettingAllCandidate_ThenItShouldReturnSuccess()
        {
            var candidates = GivenSampleCandidate();
            mockQueryBus.Setup(p => p.SendAsync(It.IsAny<GetAllCandidatesQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(candidates);
            var controller = new CandidateController(mockQueryBus.Object, mockCommandBus.Object);

            var actionResult = await controller.GetAllCandidates(CancellationToken.None);
            var okResult = actionResult as OkObjectResult;

            Assert.Equal(200, okResult?.StatusCode);
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
