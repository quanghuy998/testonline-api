using TestOnlineProject.Domain.Aggregates.TestAggregate;
using TestOnlineProject.Infrastructure.CQRS.Commands;

namespace TestOnlineProject.Application.Commands.Tests
{
    public class DeleteTestCommand : ICommand<Guid>
    {
        public Guid Id { get; init; }
    }

    public class DeleteTestCommandHandler : ICommandHandler<DeleteTestCommand, Guid>
    {
        private readonly ITestRepository _testRepository;

        public DeleteTestCommandHandler(ITestRepository testRepository)
        {
            _testRepository = testRepository;
        }
        public async Task<CommandResult<Guid>> Handle(DeleteTestCommand request, CancellationToken cancellationToken)
        {
            var test = await _testRepository.FindOneAsync(request.Id, cancellationToken);
            if (test == null) return CommandResult<Guid>.Error("The test does not exist.");

            await _testRepository.DeleteAsync(test, cancellationToken);
            return CommandResult<Guid>.Success(test.Id);
        }
    }
}
