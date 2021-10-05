using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ZinfoFramework.Repository.Interfaces
{
    public interface IRepositoryBase<TEntity> : IDisposable where TEntity : class
    {
        void Add(TEntity entity);

        void AddAll(List<TEntity> entity);

        void Edit(TEntity entity);

        void Delete(TEntity entity);

        void DeleteAll(Expression<Func<TEntity, bool>> filter = null);

        List<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null);

        TEntity Get(long id);

        List<TEntity> Where(Expression<Func<TEntity, bool>> filter, params Expression<Func<TEntity, object>>[] includes);
    }
}