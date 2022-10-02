using TestOnlineProject.Infrastructure.CQRS.Commands;
using TestOnlineProject.Domain.Aggregates.CandidateAggregate;

namespace TestOnlineProject.Application.Commands.Candidates
{
    public class DeleteCandidateCommand : ICommand<Guid>
    {
        public Guid Id { get; init; }
    }

    public class DeleteCandidateCommandHandler : ICommandHandler<DeleteCandidateCommand, Guid>
    {
        private readonly ICandidateRepository _candidateRepository;

        public DeleteCandidateCommandHandler(ICandidateRepository candidateRepository)
        {
            _candidateRepository = candidateRepository;
        }

        public async Task<CommandResult<Guid>> Handle(DeleteCandidateCommand request, CancellationToken cancellationToken)
        {
            var candidate = await _candidateRepository.FindOneAsync(request.Id, cancellationToken);
            if (candidate == null) return CommandResult<Guid>.Error("The candidate does not exist.");
            await _candidateRepository.DeleteAsync(candidate, cancellationToken);

            return CommandResult<Guid>.Success(candidate.Id);
        }
    }
}
