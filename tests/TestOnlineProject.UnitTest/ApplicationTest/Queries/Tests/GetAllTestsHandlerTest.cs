using Moq;
using Xunit;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TestOnlineProject.Application.Queries.Tests;
using TestOnlineProject.Domain.Aggregates.TestAggregate;
using TestOnlineProject.Domain.SeedWork;

namespace TestOnlineProject.UnitTest.ApplicationTest.Queries.Tests
{
    public class GetAllTestsHandlerTest
    {
        private readonly Mock<ITestRepository> mockTestRepository = new();
        private GetAllTestsQuery query = new();

        [Fact]
        public async Task GivenRequest_WhenGetingAllTests_ThenItShouldReturnTestList()
        {
            var expectedTestList = GivenSampleTestList();
            mockTestRepository.Setup(p => p.FindAllAsync(It.IsAny<BaseSpecification<Test>>(), It.IsAny<CancellationToken>())).ReturnsAsync(expectedTestList);
            var handler = new GetAllTestsQueryHandler(mockTestRepository.Object);

            var result = await handler.Handle(query, CancellationToken.None);
            var actualTestList = result as List<Test>;

            Assert.Equal(expectedTestList, actualTestList);
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
