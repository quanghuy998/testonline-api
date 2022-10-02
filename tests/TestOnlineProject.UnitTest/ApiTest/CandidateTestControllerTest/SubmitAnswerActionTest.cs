using Moq;
using Xunit;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TestOnlineProject.API.Dtos;
using TestOnlineProject.API.Controllers;
using TestOnlineProject.Infrastructure.CQRS.Commands;
using TestOnlineProject.Infrastructure.CQRS.Queries;
using static TestOnlineProject.API.Dtos.CandateTestRequest;
using TestOnlineProject.Application.Commands.CandidateTests;

namespace TestOnlineProject.UnitTest.ApiTest.CandidateTestControllerTest
{
    public class SubmitAnswerActionTest
    {
        private readonly Mock<IQueryBus> mockQueryBus = new();
        private readonly Mock<ICommandBus> mockCommandBus = new();
        private readonly SubmitAnswerRequest request = new(new List<SubmitAnswerDto>());

        [Fact]
        public async Task GivenAnswers_WhenSubmitingAnswersIntoCandidateTest_ThenItShouldReturnOk()
        {
            var id = Guid.NewGuid();
            var commandResult = CommandResult<Guid>.Success(id);
            mockCommandBus.Setup(p => p.SendAsync(It.IsAny<SubmitAnswersCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(commandResult);
            var controller = new CandidateTestController(mockQueryBus.Object, mockCommandBus.Object);

            var actionResult = await controller.SubmitAnswer(id, request, CancellationToken.None);
            var okResult = actionResult as OkObjectResult;

            Assert.Equal(200, okResult?.StatusCode);
        }
    }
}
