namespace Blog.Api.Domain
{
    public interface ICommand { }

    public interface ICommandHandler<TCommand>
        where TCommand : ICommand
    {
        void Handle(TCommand command);
    }

    public interface ICommandHandlerAsync<TCommand>
        where TCommand : ICommand
    {
        Task HandleAsync(TCommand command, CancellationToken cancellationToken = default);
    }

    public interface ICommandHandlerAsync<TCommand, TResponse>
        where TCommand : ICommand
    {
        Task<TResponse> HandleAsync(TCommand command, CancellationToken cancellationToken = default);
    }
}
