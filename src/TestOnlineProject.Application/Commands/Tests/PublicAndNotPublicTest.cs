using TestOnlineProject.Domain.Aggregates.TestAggregate;
using TestOnlineProject.Infrastructure.CQRS.Commands;

namespace TestOnlineProject.Application.Commands.Tests
{
    public class PublicAndNotPublicTestCommand : ICommand<Guid>
    {
        public Guid Id { get; init; }
        public bool IsPublic { get; init; }
    }

    public class PublicAndNotPublicTestCommandHandler : ICommandHandler<PublicAndNotPublicTestCommand, Guid>
    {
        private readonly ITestRepository _testRepository;

        public PublicAndNotPublicTestCommandHandler(ITestRepository testRepository)
        {
            _testRepository = testRepository;
        }

        public async Task<CommandResult<Guid>> Handle(PublicAndNotPublicTestCommand request, CancellationToken cancellationToken)
        {
            var test = await _testRepository.FindOneAsync(request.Id, cancellationToken);
            if (test == null) return CommandResult<Guid>.Error("The test does not exist.");

            test.PublicAndNotPublicTest(request.IsPublic);
            await _testRepository.SaveAsync(test, cancellationToken);

            return CommandResult<Guid>.Success(test.Id);
        }
    }
}
