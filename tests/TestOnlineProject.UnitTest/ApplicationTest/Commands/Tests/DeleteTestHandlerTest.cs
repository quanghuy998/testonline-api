using Moq;
using Xunit;
using System;
using System.Threading;
using System.Threading.Tasks;
using TestOnlineProject.Application.Commands.Tests;
using TestOnlineProject.Domain.Aggregates.TestAggregate;

namespace TestOnlineProject.UnitTest.ApplicationTest.Commands.Tests
{
    public class DeleteTestHandlerTest
    {
        private Mock<ITestRepository> mockTestRepository = new();
        private DeleteTestCommand command = new() { Id = Guid.NewGuid() };

        [Fact]
        public async Task GivenAnTestInformation_WhenDeletingTest_ThenItShouldReturnSuccess()
        {
            var test = GivenSampleTest();
            mockTestRepository.Setup(p => p.FindOneAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(test);
            var handler = new DeleteTestCommandHandler(mockTestRepository.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task GivenInformationOfTheTestDoesNotExist_WhenDeletingTest_ThenItShoudReturnErrorMessage()
        {
            mockTestRepository.Setup(p => p.FindOneAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(() => null);
            var handler = new DeleteTestCommandHandler(mockTestRepository.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            string message = "The test does not exist.";
            Assert.Equal(message, result.Message);
        }

        private Test GivenSampleTest()
        {
            string title = "Test 1";
            return new Test(title);
        }
    }
}
