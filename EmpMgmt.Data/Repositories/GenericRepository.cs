using EmpMgmt.Core.IRepositories;
using EmpMgmt.Data.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EmpMgmt.Data.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly DbSet<TEntity> _dbSet;

        public GenericRepository(ApplicationDbContext context)
        {
            _dbSet = context.Set<TEntity>();
        }

        public async Task<TEntity?> GetByIdAsync(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }

        public async Task AddAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }

        public void Update(TEntity entity)
        {
            _dbSet.Update(entity);
        }

        public void Remove(TEntity entity)
        {
            _dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            _dbSet.RemoveRange(entities);
        }


        public IQueryable<TEntity> GetQueryable()
        {
            return _dbSet;
        }
    }
}
