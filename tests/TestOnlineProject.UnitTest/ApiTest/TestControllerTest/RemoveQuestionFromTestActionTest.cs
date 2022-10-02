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

namespace TestOnlineProject.UnitTest.ApiTest.TestControllerTest
{
    public class RemoveQuestionFromTestActionTest
    {
        private readonly Mock<IQueryBus> mockQueryBus = new();
        private readonly Mock<ICommandBus> mockCommandBus = new();

        [Fact]
        public async Task GivenAnQuestion_WhenRemovingQuestionFromTest_ThenItShouldReturnOk()
        {
            var testId = Guid.NewGuid();
            var questionId = Guid.NewGuid();
            var commandResult = CommandResult<Guid>.Success(questionId);
            mockCommandBus.Setup(p => p.SendAsync(It.IsAny<RemoveQuestionFromTestCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(commandResult);
            var controller = new TestController(mockQueryBus.Object, mockCommandBus.Object);

            var actionResult = await controller.RemoveQuestionFromTest(testId, questionId, CancellationToken.None);
            var okResult = actionResult as OkObjectResult;

            Assert.Equal(200, okResult?.StatusCode);
        }
    }
}
