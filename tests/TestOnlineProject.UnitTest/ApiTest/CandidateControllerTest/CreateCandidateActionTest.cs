using Moq;
using Xunit;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestOnlineProject.API.Controllers;
using TestOnlineProject.Application.Commands.Candidates;
using TestOnlineProject.Infrastructure.CQRS.Commands;
using TestOnlineProject.Infrastructure.CQRS.Queries;
using static TestOnlineProject.API.Dtos.CandidateRequest;
using System;

namespace TestOnlineProject.UnitTest.ApiTest.CandidateControllerTest
{
    public class CreateCandidateActionTest
    {
        private readonly Mock<IQueryBus> mockQueryBus = new();
        private readonly Mock<ICommandBus> mockCommandBus = new();
        private readonly CreateCandidateRequest request = new("Huy", "Huynh Duc Quang", "huy@gmail.com");

        [Fact]
        public async Task GivenCandidateInformation_WhenCreatingCandidate_ThenItShouldReturnOk()
        {
            var commandResult = CommandResult<Guid>.Success(Guid.NewGuid());
            mockCommandBus.Setup(p => p.SendAsync(It.IsAny<CreateCandidateCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(commandResult);
            var controller = new CandidateController(mockQueryBus.Object, mockCommandBus.Object);

            var actionResult = await controller.CreateCandidate(request, CancellationToken.None);
            var okResult = actionResult as OkObjectResult;

            Assert.Equal(200, okResult?.StatusCode);
        }
    }
}
