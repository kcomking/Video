using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Video.Core.Interface
{
   public interface IRepository<TEntity>
    {
        #region Query
        TEntity GetByKey(int id);
        Task<TEntity> GetByKeyAsync(int id);
        IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> expression);
        IEnumerable<TEntity> QueryAll();
        Task<List<TEntity>> QueryAllAsync();
        #endregion

        #region Insert
        void Insert(TEntity entity);
        void Insert(IEnumerable<TEntity> entities);
        Task InsertAsync(TEntity entity); 
        #endregion

        #region Update
        void Update(TEntity entity);
        void Update(IEnumerable<TEntity> entities);

        #endregion

        #region Remove
        void Remove(TEntity entity);
        void Remove(Expression<Func<TEntity, bool>> expression);

        #endregion


    }
}
