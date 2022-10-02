using TestOnlineProject.Domain.SeedWork;

namespace TestOnlineProject.Domain.Aggregates.CandidateAggregate
{
    public class Candidate : AggregateRoot<Guid>
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }

        private Candidate()
        {

        }

        public Candidate(string fistName, string lastName, string email)
        {
            FirstName = fistName;
            LastName = lastName;
            Email = email;
        }
        public void UpdateCandidate(string fistName, string lastName, string email)
        {
            FirstName = fistName;
            LastName = lastName;
            Email = email;
        }
    }
}
