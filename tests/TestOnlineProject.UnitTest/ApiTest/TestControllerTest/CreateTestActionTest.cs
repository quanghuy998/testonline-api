using Moq;
using Xunit;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestOnlineProject.API.Controllers;
using TestOnlineProject.Application.Commands.Tests;
using TestOnlineProject.Infrastructure.CQRS.Commands;
using TestOnlineProject.Infrastructure.CQRS.Queries;
using static TestOnlineProject.API.Dtos.TestRequest;
using System;

namespace TestOnlineProject.UnitTest.ApiTest.TestControllerTest
{
    public class CreateTestActionTest
    {
        private readonly Mock<IQueryBus> mockQueryBus = new();
        private readonly Mock<ICommandBus> mockCommandBus = new();
        private readonly CreateTestRequest request = new CreateTestRequest("Title");

        [Fact]
        public async Task GivenInformation_WhenCreatingQuestion_ThenItShouldReturnOk()
        {
            var commandResult = CommandResult<Guid>.Success(Guid.NewGuid());
            mockCommandBus.Setup(p => p.SendAsync(It.IsAny<CreateTestCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(commandResult);
            var controller = new TestController(mockQueryBus.Object, mockCommandBus.Object);

            var actionResult = await controller.CreateTest(request, CancellationToken.None);
            var okResult = actionResult as OkObjectResult;

            Assert.Equal(200, okResult?.StatusCode);
        }
    }
}
