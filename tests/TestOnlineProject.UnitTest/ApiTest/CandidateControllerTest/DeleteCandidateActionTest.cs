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

namespace TestOnlineProject.UnitTest.ApiTest.CandidateControllerTest
{
    public class DeleteCandidateActionTest
    {
        private readonly Mock<IQueryBus> mockQueryBus = new();
        private readonly Mock<ICommandBus> mockCommandBus = new();

        [Fact]
        public async Task GivenCandidateInformation_WhenDeleteCandidate_ThenItShouldReturnOk()
        {
            var id = Guid.NewGuid();
            var commandResult = CommandResult<Guid>.Success(id);
            mockCommandBus.Setup(p => p.SendAsync(It.IsAny<DeleteCandidateCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(commandResult);
            var controller = new CandidateController(mockQueryBus.Object, mockCommandBus.Object);

            var actionResult = await controller.DeleteCandidate(id, CancellationToken.None);
            var okResult = actionResult as OkObjectResult;

            Assert.Equal(200, okResult?.StatusCode);
        }
    }
}
