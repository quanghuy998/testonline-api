using MediatR;
using Microsoft.Extensions.DependencyInjection;
using TestOnlineProject.Domain.SeedWork;

namespace TestOnlineProject.Infrastructure.CQRS.Commands
{
    public class CommandBus : ICommandBus
    {
        private readonly IMediator _mediator;
        private readonly IUnitOfWork _unitOfWork;

        public CommandBus(IServiceProvider serviceProvider)
        {
            _unitOfWork = serviceProvider.GetRequiredService<IUnitOfWork>();
            _mediator = serviceProvider.GetRequiredService<IMediator>();
        }

        public Task<CommandResult> SendAsync(ICommand command, CancellationToken cancellationToken)
        {
            return _unitOfWork.ExecuteAsync(() => _mediator.Send(command, cancellationToken), cancellationToken);
        }

        public Task<CommandResult<TResponse>> SendAsync<TResponse>(ICommand<TResponse> command, CancellationToken cancellationToken)
        {
            return _unitOfWork.ExecuteAsync(() => _mediator.Send(command, cancellationToken), cancellationToken);
        }
    }
}
