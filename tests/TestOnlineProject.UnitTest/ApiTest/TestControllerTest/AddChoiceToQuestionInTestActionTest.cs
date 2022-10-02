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
    public class AddChoiceToQuestionInTestActionTest
    {
        private readonly Mock<IQueryBus> mockQueryBus = new();
        private readonly Mock<ICommandBus> mockCommandBus = new();
        private AddChoiceToQuestionInTestRequest request = new AddChoiceToQuestionInTestRequest("Choice number 1", true);

        [Fact]
        public async Task GivenAChoice_WhenAddingChoiceToQuestion_ThenItShouldReturnOk()
        {
            var testId = Guid.NewGuid();
            var questionId = Guid.NewGuid();
            var commandResult = CommandResult<Guid>.Success(Guid.NewGuid());
            mockCommandBus.Setup(p => p.SendAsync(It.IsAny<AddChoiceToQuestionInTestCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(commandResult);
            
            var controller = new TestController(mockQueryBus.Object, mockCommandBus.Object);
            var actionResult = await controller.AddChoiceToQuestionInTest(testId, questionId, request, CancellationToken.None);
            var okResult = actionResult as OkObjectResult;

            Assert.Equal(200, okResult?.StatusCode);
        }
    }
}
