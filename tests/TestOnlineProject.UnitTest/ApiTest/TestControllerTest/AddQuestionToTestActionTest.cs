using Moq;
using Xunit;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestOnlineProject.API.Controllers;
using TestOnlineProject.Application.Commands.Tests;
using TestOnlineProject.Domain.Aggregates.TestAggregate;
using TestOnlineProject.Infrastructure.CQRS.Commands;
using TestOnlineProject.Infrastructure.CQRS.Queries;
using static TestOnlineProject.API.Dtos.TestRequest;

namespace TestOnlineProject.UnitTest.ApiTest.TestControllerTest
{
    public class AddQuestionToTestActionTest
    {
        private readonly Mock<IQueryBus> mockQueryBus = new();
        private readonly Mock<ICommandBus> mockCommandBus = new();

        [Fact]
        public async Task GivenAnQuestion_WhenAddingQuestionToTest_ThenItShouldReturnOk()
        {
            var testId = Guid.NewGuid();
            var request = new AddQuestionToTestRequest("Question text", 30, 10, QuestionType.MultipChoice);
            var commandResult = CommandResult<Guid>.Success(Guid.NewGuid());

            mockCommandBus.Setup(p => p.SendAsync(It.IsAny<AddQuestionToTestCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(commandResult);
            var controller = new TestController(mockQueryBus.Object, mockCommandBus.Object);
            var actionResult = await controller.AddQuestionToTest(testId, request, CancellationToken.None);
            var okResult = actionResult as OkObjectResult;

            Assert.Equal(200, okResult?.StatusCode);
        }
    }
}
