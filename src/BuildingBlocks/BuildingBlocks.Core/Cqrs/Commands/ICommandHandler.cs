using MediatR;

namespace BuildingBlocks.Core.Cqrs.Commands
{
    public interface ICommandHandler<in TRequest> : IRequestHandler<TRequest> where TRequest : ICommand
    {
    }

    public interface ICommandHandler<in TRequest, TResponse> : IRequestHandler<TRequest, TResponse> where TRequest : ICommand<TResponse>
    {
    }
}