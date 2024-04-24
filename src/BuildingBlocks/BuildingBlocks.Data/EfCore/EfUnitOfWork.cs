using BuildingBlocks.Core.Domain;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;

namespace BuildingBlocks.Data.EfCore
{
    public class EfUnitOfWork : IUnitOfWork
    {
        private readonly BaseDbContext _baseDbContext;

        public EfUnitOfWork(BaseDbContext baseDbContext)
        {
            _baseDbContext = baseDbContext;
        }

        public IEnumerable<IEntity> GetEntities()
        {
            return _baseDbContext.ChangeTracker.Entries<IEntity>().Select(x => x.Entity);
        }

        public int SaveChanges()
        {
            return _baseDbContext.SaveChanges();
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _baseDbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        {
            return await _baseDbContext.BeginTransactionAsync(isolationLevel);
        }

        public async Task CommitTransactionAsync(IDbContextTransaction? transaction)
        {
            await _baseDbContext.CommitTransactionAsync(transaction);
        }

        public IExecutionStrategy CreateExecutionStrategy()
        {
            return _baseDbContext.CreateExecutionStrategy();
        }

        public bool HasActiveTransaction => _baseDbContext.HasActiveTransaction;

        public string TransactionId => _baseDbContext.GetCurrentTransaction()?.TransactionId.ToString()!;
    }
}