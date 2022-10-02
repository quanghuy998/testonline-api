using Xunit;
using TestOnlineProject.Domain.Aggregates.CandidateAggregate;

namespace TestOnlineProject.UnitTest.DomainTest
{
    public class CandidateAggregateTest
    {
        [Fact]
        public void GivenInformation_WhenCreatingCandidate_ThenItShouldBeCreated()
        {
            string firstName = "Musso";
            string lastName = "Guillaume";
            string email = "musso@mail.com";

            var candidate = new Candidate(firstName, lastName, email);
            
            Assert.True(firstName.Equals(candidate.FirstName));
            Assert.True(lastName.Equals(candidate.LastName));
            Assert.True(email.Equals(candidate.Email));
        }

        [Fact]
        public void GivenInformation_WhenUpdatetingCandidate_ThenItShouldBeUpdated()
        {
            var candidate = GivenSampleCandidate();
            string firstName = "Marc";
            string lastName = "Levy";
            string email = "levy@mail.com";

            candidate.UpdateCandidate(firstName, lastName, email);

            Assert.True(firstName.Equals(candidate.FirstName));
            Assert.True(lastName.Equals(candidate.LastName));
            Assert.True(email.Equals(candidate.Email));
        }

        public Candidate GivenSampleCandidate()
        {
            string firstName = "Musso";
            string lastName = "Guillaume";
            string email = "musso@mail.com";

            return new Candidate(firstName, lastName, email);
        }
    }
}
