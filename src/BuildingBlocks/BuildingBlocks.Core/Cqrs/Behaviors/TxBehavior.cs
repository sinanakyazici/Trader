using BuildingBlocks.Core.Cqrs.Commands;
using BuildingBlocks.Core.Domain;
using BuildingBlocks.Core.Event;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BuildingBlocks.Core.Cqrs.Behaviors;

public class TxBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDomainEventService _domainEventService;
    private readonly IIntegrationEventService _integrationEventService;

    public TxBehavior(
        IUnitOfWork unitOfWork,
        IDomainEventService domainEventService,
        IIntegrationEventService integrationEventService)
    {
        _unitOfWork = unitOfWork;
        _domainEventService = domainEventService;
        _integrationEventService = integrationEventService;
    }
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (request is not ICommand<TResponse> && request is not ICommand) return await next();
        var response = default(TResponse);
        if (_unitOfWork.HasActiveTransaction)
        {
            response = await next();
            // save the data for using by another request via database
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return response;
        }

        // An execution strategy that automatically retries on failures needs to be able to play back each operation in a retry block that fails. 
        var strategy = _unitOfWork.CreateExecutionStrategy();

        // Connection resiliency automatically retries failed database commands. 
        await strategy.ExecuteAsync(async () =>
        {
            await using var transaction = await _unitOfWork.BeginTransactionAsync();
            response = await next();
            // save the data for using by another request via database
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            // domain events cannot share data via database. They have to get the values as input from outside. 
            await _domainEventService.DispatchEventsAsync();
            // commit all pending changes
            await _unitOfWork.CommitTransactionAsync(transaction);

            await _integrationEventService.DispatchEventsAsync(transaction.TransactionId.ToString());
        });

        return response!;
    }
}