namespace TestOnlineProject.Infrastructure.CQRS.Commands
{
    public class CommandResult
    {
        public bool IsSuccess { get; protected set; }
        public string Message { get; protected set; }

        protected CommandResult()
        {
        }

        public static CommandResult Success()
        {
            return new CommandResult { IsSuccess = true };
        }

        public static CommandResult Error(string message = null)
        {
            return new CommandResult { Message = message };
        }
    }

    public class CommandResult<TResponse> : CommandResult
    {
        public TResponse Response { get; init; }

        public static CommandResult<TResponse> Success(TResponse response)
        {
            return new CommandResult<TResponse> { IsSuccess = true, Response = response };
        }

        public static CommandResult<TResponse> Error(string message = null)
        {
            return new CommandResult<TResponse> { Message = message };
        }
    }
}
