using MediatR;

namespace BuildingBlocks.Core.Cqrs.Queries
{
    public interface IQuery<out TResponse> : IRequest<TResponse>
    {
    }
}