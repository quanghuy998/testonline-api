using TestOnlineProject.Infrastructure.CQRS.Commands;
using TestOnlineProject.Domain.Aggregates.CandidateAggregate;

namespace TestOnlineProject.Application.Commands.Candidates
{
    public class CreateCandidateCommand : ICommand<Guid>
    {
        public string FirstName { get; init; }
        public string LastName { get; init; }
        public string Email { get; init; }
    }

    public class CreateCandidateCommandHandler : ICommandHandler<CreateCandidateCommand, Guid>
    {
        private readonly ICandidateRepository _candidateRepository;

        public CreateCandidateCommandHandler(ICandidateRepository candidateRepository)
        {
            _candidateRepository = candidateRepository;
        }

        public async Task<CommandResult<Guid>> Handle(CreateCandidateCommand request, CancellationToken cancellationToken)
        {
            var candidate = new Candidate(request.FirstName, request.LastName, request.Email);
            await _candidateRepository.AddAsync(candidate, cancellationToken);

            return CommandResult<Guid>.Success(candidate.Id);
        }
    }
}
