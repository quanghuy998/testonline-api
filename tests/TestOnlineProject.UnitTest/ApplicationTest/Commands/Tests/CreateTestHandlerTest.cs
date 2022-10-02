using Moq;
using Xunit;
using System.Threading;
using TestOnlineProject.Application.Commands.Tests;
using TestOnlineProject.Domain.Aggregates.TestAggregate;

namespace TestOnlineProject.UnitTest.ApplicationTest.Commands.Tests
{
    public class CreateTestHandlerTest
    {
        private Mock<ITestRepository> mockTestRepository = new();
        private CreateTestCommand command = new() { Title = "Is this a title?" };

        [Fact]
        public async void GivenInformation_WhenCreatingAnTest_ThenItShouldReturnSuccess()
        {
            var handler = new CreateTestCommandHandler(mockTestRepository.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            Assert.True(result.IsSuccess);
        }
    }
}
