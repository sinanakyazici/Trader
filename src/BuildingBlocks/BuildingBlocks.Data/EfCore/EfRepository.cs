using BuildingBlocks.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;

namespace BuildingBlocks.Data.EfCore
{
    public abstract class EfRepository<TEntity> : ICommandRepository<TEntity>
        where TEntity : class, IEntity
    {

        private readonly BaseDbContext _dbContext;
        private readonly DbSet<TEntity> _dbSet;


        protected EfRepository(BaseDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<TEntity>();
        }


        public virtual EntityEntry<TEntity> Add(TEntity obj)
        {
            return _dbSet.Add(obj);
        }

        public virtual void AddRange(params TEntity[] entities)
        {
            _dbSet.AddRange(entities);
        }

        public virtual void AddRange(IEnumerable<TEntity> entities)
        {
            _dbSet.AddRange(entities);
        }

        public virtual ValueTask<EntityEntry<TEntity>> AddAsync(TEntity obj, CancellationToken cancellationToken)
        {
            return _dbSet.AddAsync(obj, cancellationToken);
        }

        public virtual Task AddRangeAsync(params TEntity[] entities)
        {
            return _dbSet.AddRangeAsync(entities);
        }

        public virtual Task AddRangeAsync(IEnumerable<TEntity> obj)
        {
            return _dbSet.AddRangeAsync(obj);
        }

        public virtual EntityEntry<TEntity> Update(TEntity obj)
        {
            return _dbSet.Update(obj);
        }

        public virtual void Update(params TEntity[] entities)
        {
            _dbSet.UpdateRange(entities);
        }

        public virtual void Update(IEnumerable<TEntity> entities)
        {
            _dbSet.UpdateRange(entities);
        }

        public virtual EntityEntry<TEntity> Remove(TEntity obj)
        {
            return _dbContext.Remove(obj);
        }

        public virtual void RemoveRange(params TEntity[] entities)
        {
            _dbContext.RemoveRange(entities.AsEnumerable() ?? throw new InvalidOperationException());
        }

        public virtual void RemoveRange(IEnumerable<TEntity> entities)
        {
            _dbContext.RemoveRange(entities);
        }

        public virtual EntityEntry<TEntity> Delete(TEntity obj)
        {
            return _dbContext.Delete(obj);
        }

        public virtual void DeleteRange(params TEntity[] entities)
        {
            _dbContext.DeleteRange(entities);
        }

        public virtual void DeleteRange(IEnumerable<TEntity> entities)
        {
            _dbContext.DeleteRange(entities);
        }

        public IDbContextTransaction? GetCurrentTransaction()
        {
            return _dbContext.GetCurrentTransaction();
        }

        protected IQueryable<TEntity> Query()
        {
            return _dbSet.AsQueryable();
        }

        protected async Task<int> ExecuteSqlRawAsync(string sql, IEnumerable<object> parameters, CancellationToken cancellationToken)
        {
            return await _dbContext.Database.ExecuteSqlRawAsync(sql, parameters, cancellationToken);
        }

        public void Dispose()
        {
            _dbContext.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}