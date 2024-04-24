using MediatR;

namespace BuildingBlocks.Core.Cqrs.Queries
{
    public interface IQueryHandler<in TQuery, TResult> : IRequestHandler<TQuery, TResult> where TQuery : IQuery<TResult>
    {

    }
}