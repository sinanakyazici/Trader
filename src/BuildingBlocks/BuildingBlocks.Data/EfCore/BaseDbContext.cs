using BuildingBlocks.Core.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Data;
using BuildingBlocks.Core.Domain;

namespace BuildingBlocks.Data.EfCore;

public abstract class BaseDbContext : DbContext
{
    private readonly IConfiguration _configuration;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ILoggerFactory _loggerFactory;
    private IDbContextTransaction? _currentTransaction;

    protected BaseDbContext(IConfiguration configuration, IHttpContextAccessor httpContextAccessor, ILoggerFactory loggerFactory)
    {
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        _loggerFactory = loggerFactory;
    }

    public bool HasActiveTransaction => _currentTransaction != null;

    public IDbContextTransaction? GetCurrentTransaction() => _currentTransaction;

    private void UpdateCreationAudited(object entity)
    {
        if (entity is not IAuditEntity item) return;
        item.CreatedDate = DateExtensions.DateTimeNow;
        var createdBy = _httpContextAccessor.HttpContext?.GetCurrentUserId();
        item.CreatedBy = createdBy ?? "system";
    }

    private void UpdateModificationAudited(object entity)
    {
        if (entity is not IAuditEntity item) return;
        item.LastModifiedDate = DateExtensions.DateTimeNow;
        var lastModifiedBy = _httpContextAccessor.HttpContext?.GetCurrentUserId();
        item.LastModifiedBy = lastModifiedBy ?? "system";
    }

    private void UpdateValidationAudited(object entity)
    {
        if (entity is not IAuditEntity item) return;
        item.ValidFor = DateExtensions.DateTimeNow;
    }

    public override void RemoveRange(IEnumerable<object> entities)
    {
        var enumerable = entities.ToList();
        foreach (var entity in enumerable) UpdateValidationAudited(entity);

        base.UpdateRange(enumerable);
    }

    public override EntityEntry<TEntity> Remove<TEntity>(TEntity entity)
    {
        foreach (var navigationEntry in Entry(entity).Navigations)
        {
            if (navigationEntry is CollectionEntry collectionEntry)
            {
                if (collectionEntry.CurrentValue != null)
                    foreach (var dependentEntry in collectionEntry.CurrentValue)
                        UpdateValidationAudited(dependentEntry);
            }
            else
            {
                var dependentEntry = navigationEntry.CurrentValue;
                if (dependentEntry != null) UpdateValidationAudited(dependentEntry);
            }
        }

        UpdateValidationAudited(entity);
        return base.Update(entity);
    }

    public void DeleteRange(IEnumerable<object> entityList)
    {
        base.RemoveRange(entityList);
    }

    public EntityEntry<TEntity> Delete<TEntity>(TEntity entity) where TEntity : class
    {
        return base.Remove(entity);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = _configuration.GetConnectionString("Command");
        optionsBuilder.UseLoggerFactory(_loggerFactory).EnableSensitiveDataLogging().UseNpgsql(connectionString);
        base.OnConfiguring(optionsBuilder);
    }

    public override int SaveChanges()
    {
        var addedNewEntities = ChangeTracker.Entries().Where(x => x.State == EntityState.Added);
        foreach (var ee in addedNewEntities) UpdateCreationAudited(ee.Entity);

        var updatedNewEntities = ChangeTracker.Entries().Where(x => x.State == EntityState.Modified);
        foreach (var ee in updatedNewEntities) UpdateModificationAudited(ee.Entity);

        return base.SaveChanges();
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var addedNewEntities = ChangeTracker.Entries().Where(x => x.State == EntityState.Added);
        foreach (var ee in addedNewEntities) UpdateCreationAudited(ee.Entity);

        var updatedNewEntities = ChangeTracker.Entries().Where(x => x.State == EntityState.Modified);
        foreach (var ee in updatedNewEntities) UpdateModificationAudited(ee.Entity);
 
        return await base.SaveChangesAsync(cancellationToken);
    }

    public async Task<IDbContextTransaction> BeginTransactionAsync(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
    {
        if (_currentTransaction != null) throw new InvalidOperationException($"Transaction {_currentTransaction.TransactionId} is already active.");
        _currentTransaction = await Database.BeginTransactionAsync(isolationLevel);
        return _currentTransaction;
    }

    public async Task CommitTransactionAsync(IDbContextTransaction? transaction)
    {
        CheckTransaction(transaction);

        try
        {
            await SaveChangesAsync();
            await transaction!.CommitAsync();
        }
        catch
        {
            await RollbackTransaction();
            throw;
        }
        finally
        {
            _currentTransaction?.Dispose();
            _currentTransaction = null;
        }
    }

    private async Task RollbackTransaction()
    {
        try
        {
            if (_currentTransaction != null)
            {
                await _currentTransaction.RollbackAsync();
            }
        }
        finally
        {
            _currentTransaction?.Dispose();
            _currentTransaction = null;
        }
    }

    public IExecutionStrategy CreateExecutionStrategy()
    {
        return Database.CreateExecutionStrategy();
    }

    private void CheckTransaction(IDbContextTransaction? transaction)
    {
        if (transaction == null) throw new ArgumentNullException(nameof(transaction));
        if (transaction != _currentTransaction) throw new InvalidOperationException($"Transaction {transaction.TransactionId} is not current");
    }
}