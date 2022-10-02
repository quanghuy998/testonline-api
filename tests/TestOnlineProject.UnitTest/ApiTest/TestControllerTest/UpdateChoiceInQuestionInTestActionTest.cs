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
    public class UpdateChoiceInQuestionInTestActionTest
    {
        private readonly Mock<IQueryBus> mockQueryBus = new();
        private readonly Mock<ICommandBus> mockCommandBus = new();
        private readonly UpdateChoiceInQuestionInTestRequest request = new UpdateChoiceInQuestionInTestRequest("Choice 2", true);

        [Fact]
        public async Task GivenInformation_WhenUpdatingChoiceInQuestion_ThenItShouldReturnOk()
        {
            var testId = Guid.NewGuid();
            var questionId = Guid.NewGuid();
            var choiceId = Guid.NewGuid();
            var commandResult = CommandResult<Guid>.Success(choiceId);
            mockCommandBus.Setup(p => p.SendAsync(It.IsAny<UpdateChoiceInQuestionInTestCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(commandResult);
            var controller = new TestController(mockQueryBus.Object, mockCommandBus.Object);

            var actionResult = await controller.UpdateChoiceInQuestionInTest(testId, questionId, choiceId, request, CancellationToken.None);
            var okResult = actionResult as OkObjectResult;

            Assert.Equal(200, okResult?.StatusCode);
        }
    }
}
