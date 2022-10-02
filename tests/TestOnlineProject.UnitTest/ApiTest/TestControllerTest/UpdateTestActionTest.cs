using Moq;
using Xunit;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestOnlineProject.API.Controllers;
using TestOnlineProject.Application.Commands.Tests;
using TestOnlineProject.Infrastructure.CQRS.Commands;
using TestOnlineProject.Infrastructure.CQRS.Queries;
using static TestOnlineProject.API.Dtos.TestRequest;

namespace TestOnlineProject.UnitTest.ApiTest.TestControllerTest
{
    public class UpdateTestActionTest
    {
        private readonly Mock<IQueryBus> mockQueryBus = new();
        private readonly Mock<ICommandBus> mockCommandBus = new();
        private readonly UpdateTestRequest request = new UpdateTestRequest("Update test");

        [Fact]
        public async Task GivenInformation_WhenUpdatingTest_ThenItShouldReturnOk()
        {
            var id = Guid.NewGuid();
            var commandResult = CommandResult<Guid>.Success(id);
            mockCommandBus.Setup(p => p.SendAsync(It.IsAny<UpdateTestCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(commandResult);
            var controller = new TestController(mockQueryBus.Object, mockCommandBus.Object);

            var actionResult = await controller.UpdateTest(id, request, CancellationToken.None);
            var okResult = actionResult as OkObjectResult;

            Assert.Equal(200, okResult?.StatusCode);
        }
    }
}
