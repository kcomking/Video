using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Video.Core.Entities;
using Video.Core.Interface;

namespace Video.Infrastructrue.Repository
{
    public class EfRepository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        #region ctor
        public EfRepository(DbContext dbContext)
        { 
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<TEntity>();
        }

         
        #endregion

        #region fields
        public readonly DbSet<TEntity> _dbSet;
        public readonly DbContext _dbContext;
        #endregion

        #region query
        public TEntity GetByKey(int key)
        {
            return _dbSet.Find(key);
        }

        public async Task<TEntity> GetByKeyAsync(int key)
        {
            return await _dbSet.FindAsync(key);
        }

        public IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> expression)
        {
            return _dbSet.Where(expression);
        }

        public IEnumerable<TEntity> QueryAll()
        {
            return _dbSet.ToList();
        }

        public Task<List<TEntity>> QueryAllAsync()
        {
            return _dbSet.ToListAsync();
        }

        #endregion

        #region insert
        public void Insert(TEntity entity)
        {
            _dbSet.Add(entity);
             
        }

        public void Insert(IEnumerable<TEntity> entities)
        {
            _dbSet.AddRange(entities);
        }

        public async Task InsertAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task InsertAsync(IEnumerable<TEntity> entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }

        #endregion

        #region update
        public void Remove(TEntity entity)
        {
            _dbSet.Remove(entity);
        }

        public void Remove(Expression<Func<TEntity, bool>> expression)
        {
            var entities = _dbSet.AsNoTracking().Where(expression).ToList();
            _dbSet.RemoveRange(entities);
        }

        #endregion

        #region remove
        public void Update(TEntity entity)
        {
            _dbSet.Update(entity);
        }

        public void Update(IEnumerable<TEntity> entities)
        {
            
            _dbSet.UpdateRange(entities);
        }

       
        #endregion

    }
}