using MediatR;

namespace Shared.CQRS
{
    public interface ICommandHandler<in TCommand> : ICommandHandler<TCommand, Unit> //commandHandler que não tem um tipo de retorno 
        where TCommand : ICommand<Unit>
    {
    }
    public interface ICommandHandler<in TCommand, TResponse> : IRequestHandler<TCommand, TResponse> //commandHandler que tem um tipo de retorno
        where TCommand : ICommand<TResponse>
        where TResponse : notnull
    {
    }
}
