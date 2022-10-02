using Moq;
using Xunit;
using System;
using System.Threading;
using System.Threading.Tasks;
using TestOnlineProject.Application.Queries.Tests;
using TestOnlineProject.Domain.Aggregates.TestAggregate;

namespace TestOnlineProject.UnitTest.ApplicationTest.Queries.Tests
{
    public class GetTestDetailsHandlerTest
    {
        private readonly Mock<ITestRepository> mockTestRepository = new();
        private GetTestDetailsQuery query = new() { Id = Guid.NewGuid() };

        [Fact]
        public async Task GivenInformation_WhenGettingTestDetails_ThenItShouldReturnTest()
        {
            var expectedTest = GivenSampleTest();
            mockTestRepository.Setup(p => p.FindOneAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(expectedTest);
            var handler = new GetTestDetailsQueryHandler(mockTestRepository.Object);

            var actualTest = await handler.Handle(query, CancellationToken.None);

            Assert.Equal(expectedTest, actualTest);
        }

        [Fact]
        public async Task GivenInformation_WhenGettingDetailsOfTheTestDoesNotExist_ThenItShouldReturnNull()
        {
            mockTestRepository.Setup(p => p.FindOneAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(() => null);
            var handler = new GetTestDetailsQueryHandler(mockTestRepository.Object);

            var result = await handler.Handle(query, CancellationToken.None);

            Assert.Null(result);
        }

        private Test GivenSampleTest()
        {
            string title = "Test 1";
            return new Test(title);
        }
    }
}
