using Moq;
using Xunit;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestOnlineProject.API.Controllers;
using TestOnlineProject.Application.Commands.CandidateTests;
using TestOnlineProject.Infrastructure.CQRS.Commands;
using TestOnlineProject.Infrastructure.CQRS.Queries;
using static TestOnlineProject.API.Dtos.CandateTestRequest;

namespace TestOnlineProject.UnitTest.ApiTest.CandidateTestControllerTest
{
    public class CreateCandidateTestActionTest
    {
        private readonly Mock<IQueryBus> mockQueryBus = new();
        private readonly Mock<ICommandBus> mockCommandBus = new();
        private readonly CreateCandidateTestRequest request = new(Guid.NewGuid(), Guid.NewGuid());

        [Fact]
        public async Task GivenCandidateTestInformation_WhenCreatingCandidateTest_ThenItShouldReturnOk()
        {
            var commandResult = CommandResult<Guid>.Success(Guid.NewGuid());
            mockCommandBus.Setup(p => p.SendAsync(It.IsAny<CreateCandidateTestCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(commandResult);
            var controller = new CandidateTestController(mockQueryBus.Object, mockCommandBus.Object);

            var actionResult = await controller.CreateCandidateTest(request, CancellationToken.None);
            var okResult = actionResult as OkObjectResult;

            Assert.Equal(200, okResult?.StatusCode);
        }
    }
}
