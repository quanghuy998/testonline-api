using Moq;
using Xunit;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TestOnlineProject.API.Controllers;
using TestOnlineProject.Application.Queries.Tests;
using TestOnlineProject.Domain.Aggregates.TestAggregate;
using TestOnlineProject.Infrastructure.CQRS.Commands;
using TestOnlineProject.Infrastructure.CQRS.Queries;

namespace TestOnlineProject.UnitTest.ApiTest.TestControllerTest
{
    public class GetAllTestsActionTest
    {
        private readonly Mock<IQueryBus> mockQueryBus = new();
        private readonly Mock<ICommandBus> mockCommandBus = new();

        [Fact]
        public async Task GivenRequest_WhenGettingAllQuestion_ThenItShouldReturnOk()
        {
            var expected = GivenSampleTestList();
            mockQueryBus.Setup(p => p.SendAsync(It.IsAny<GetAllTestsQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(expected);
            var controller = new TestController(mockQueryBus.Object, mockCommandBus.Object);

            var actionResult = await controller.GetAllTests(CancellationToken.None);
            var okResult = actionResult as OkObjectResult;
            var actual = okResult?.Value as List<Test>;

            Assert.Equal(200, okResult?.StatusCode);
            Assert.Equal(expected, actual);
        }

        private List<Test> GivenSampleTestList()
        {
            return new List<Test>
            {
                new Test("Title 1"),
                new Test("Title 2"),
                new Test("Title 3")
            };
        }
    }
}
