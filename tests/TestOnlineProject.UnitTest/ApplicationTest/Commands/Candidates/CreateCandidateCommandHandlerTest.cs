using Moq;
using Xunit;
using System.Threading;
using TestOnlineProject.Application.Commands.Candidates;
using TestOnlineProject.Domain.Aggregates.CandidateAggregate;

namespace TestOnlineProject.UnitTest.ApplicationTest.Commands.Candidates
{
    public class CreateCandidateCommandHandlerTest
    {
        private readonly Mock<ICandidateRepository> mockCandidateRepository = new();
        private readonly CreateCandidateCommand command = new CreateCandidateCommand()
        {
            FirstName = "Musso",
            LastName = "Guillaume",
            Email = "guillaumemusso@gmail.com",
        };
        
        [Fact]
        public async void GivenInformation_WhenCreatingCandidate_ThenItShouldReturnSuccess()
        {
            var handler = new CreateCandidateCommandHandler(mockCandidateRepository.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            Assert.True(result.IsSuccess);
        }
    }
}
