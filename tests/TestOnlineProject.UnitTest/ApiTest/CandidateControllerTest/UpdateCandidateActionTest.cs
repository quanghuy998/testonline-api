using Moq;
using Xunit;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestOnlineProject.API.Controllers;
using TestOnlineProject.Application.Commands.Candidates;
using TestOnlineProject.Infrastructure.CQRS.Commands;
using TestOnlineProject.Infrastructure.CQRS.Queries;
using static TestOnlineProject.API.Dtos.CandidateRequest;

namespace TestOnlineProject.UnitTest.ApiTest.CandidateControllerTest
{
    public class UpdateCandidateActionTest
    {
        private readonly Mock<IQueryBus> mockQueryBus = new();
        private readonly Mock<ICommandBus> mockCommandBus = new();
        private readonly UpdateCandidateRequest request = new("Huy", "Huynh Duc Quang", "huy@gmail.com");

        [Fact]
        public async Task GivenCandidateInformation_WhenUpdatingCandidate_ThenItShouldReturnOk()
        {
            var id = Guid.NewGuid();
            var commandResult = CommandResult<Guid>.Success(id);
            mockCommandBus.Setup(p => p.SendAsync(It.IsAny<UpdateCandidateCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(commandResult);
            var controller = new CandidateController(mockQueryBus.Object, mockCommandBus.Object);

            var actionResult = await controller.UpdateCandidate(id, request, CancellationToken.None);
            var okResult = actionResult as OkObjectResult;

            Assert.Equal(200, okResult?.StatusCode);
        }
    }
}
