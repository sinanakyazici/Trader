using System.Data;
using Microsoft.EntityFrameworkCore.Storage;

namespace BuildingBlocks.Core.Domain
{
    public interface IUnitOfWork
    {
        IEnumerable<IEntity> GetEntities();
        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        Task<IDbContextTransaction> BeginTransactionAsync(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted);
        Task CommitTransactionAsync(IDbContextTransaction? transaction);
        IExecutionStrategy CreateExecutionStrategy();
        bool HasActiveTransaction { get; }
        string TransactionId { get; }
    }
}