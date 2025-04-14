using MediatR;

namespace Shared.CQRS
{
    public interface ICommand : ICommand<Unit> // vem do mediatR e representa um type void
    {
    }   
    public interface ICommand<out TResponse> : IRequest<TResponse>
    {
    }
}
