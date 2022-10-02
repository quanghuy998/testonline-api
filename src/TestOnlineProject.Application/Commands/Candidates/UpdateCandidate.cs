using TestOnlineProject.Infrastructure.CQRS.Commands;
using TestOnlineProject.Domain.Aggregates.CandidateAggregate;

namespace TestOnlineProject.Application.Commands.Candidates
{
    public class UpdateCandidateCommand : ICommand<Guid>
    {
        public Guid Id { get; init; }
        public string FirstName { get; init; }
        public string LastName { get; init; }
        public string Email { get; init; }
    }

    public class UpdateCandidateCommandHandler : ICommandHandler<UpdateCandidateCommand, Guid>
    {
        private readonly ICandidateRepository _candidateRepository;

        public UpdateCandidateCommandHandler(ICandidateRepository candidateRepository)
        {
            _candidateRepository = candidateRepository;
        }

        public async Task<CommandResult<Guid>> Handle(UpdateCandidateCommand request, CancellationToken cancellationToken)
        {
            var candidate = await _candidateRepository.FindOneAsync(request.Id, cancellationToken);
            if (candidate == null) return CommandResult<Guid>.Error("The candidate does not exist.");

            candidate.UpdateCandidate(request.FirstName, request.LastName, request.Email);
            await _candidateRepository.SaveAsync(candidate, cancellationToken);

            return CommandResult<Guid>.Success(candidate.Id);
        }
    }
}
