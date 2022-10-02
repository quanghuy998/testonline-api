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
    public class UpdateQuestionActionTest
    {
        private readonly Mock<IQueryBus> mockQueryBus = new();
        private readonly Mock<ICommandBus> mockCommandBus = new();
        private readonly UpdateQuestionInTestRequest request = new UpdateQuestionInTestRequest("Question number 1", 30, 5, QuestionType.MultipChoice);

        [Fact]
        public async Task GivenInformation_WhenUpdatingQuestion_ThenItShouldReturnOk()
        {
            var testId = Guid.NewGuid();
            var questionId = Guid.NewGuid();
            var commandResult = CommandResult<Guid>.Success(questionId);
            mockCommandBus.Setup(p => p.SendAsync(It.IsAny<UpdateQuestionInTestCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(commandResult);
            var controller = new TestController(mockQueryBus.Object, mockCommandBus.Object);

            var actionResult = await controller.UpdateQuestionInTest(testId, questionId, request, CancellationToken.None);
            var okResult = actionResult as OkObjectResult;

            Assert.Equal(200, okResult?.StatusCode);
        }
    }
}
