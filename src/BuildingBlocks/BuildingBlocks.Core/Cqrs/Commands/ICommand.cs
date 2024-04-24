using MediatR;

namespace BuildingBlocks.Core.Cqrs.Commands
{
    public interface ICommand : IRequest
    {
    }

    public interface ICommand<out TResult> : IRequest<TResult>
    {
    }
}