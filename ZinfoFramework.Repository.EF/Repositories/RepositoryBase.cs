using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using ZinfoFramework.Repository.Interfaces;

namespace ZinfoFramework.Repository.EF.Repositories
{
    public abstract class RepositoryBase<TEntity> : IRepositoryBase<TEntity>
        where TEntity : class
    {
        protected DbContext Context { get; }

        public RepositoryBase(DbContext context)
        {
            Context = context;
        }

        public void Add(TEntity entity)
        {
            DbSet<TEntity> dbSet = Context.Set<TEntity>();
            dbSet.Add(entity);
            Context.SaveChanges();
        }

        /// <summary>
        /// Método que adiciona uma lista de novos objetos ao banco de dados da aplicação.
        /// </summary>
        public void AddAll(List<TEntity> entity)
        {
            DbSet<TEntity> dbSet = Context.Set<TEntity>();

            foreach (var item in entity)
            {
                dbSet.Add(item);
            }
            Context.SaveChanges();
        }

        public void Delete(TEntity entity)
        {
            DbSet<TEntity> dbSet = Context.Set<TEntity>();

            if (Context.Entry(entity).State == EntityState.Detached)
            {
                dbSet.Attach(entity);
            }
            dbSet.Remove(entity);
            Context.SaveChanges();
        }

        /// <summary>
        /// Método que deleta um ou varios objetos no banco de dados da aplicação, mediante uma expressão LINQ.
        /// </summary>
        public void DeleteAll(Expression<Func<TEntity, bool>> filter = null)
        {
            DbSet<TEntity> dbSet = Context.Set<TEntity>();

            IQueryable<TEntity> query = dbSet;
            List<TEntity> listDelete = query.Where(filter).ToList();

            foreach (var item in listDelete)
            {
                dbSet.Remove(item);
            }
            Context.SaveChanges();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public void Edit(TEntity entity)
        {
            DbSet<TEntity> dbSet = Context.Set<TEntity>();

            dbSet.Update(entity);
            Context.SaveChanges();
        }

        public virtual List<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null)
        {
            DbSet<TEntity> dbSet = Context.Set<TEntity>();

            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        public TEntity Get(long id)
        {
            DbSet<TEntity> dbSet = Context.Set<TEntity>();

            return dbSet.Find(id);
        }

        public bool Existe(Expression<Func<TEntity, bool>> filter)
        {
            DbSet<TEntity> dbSet = Context.Set<TEntity>();

            return dbSet.AsNoTracking().Any(filter);
        }

        /// <summary>
        /// Método que busca uma lista de objetos no banco de dados da aplicação e retorna-a no tipo IEnumerable<TEntity>.
        /// </summary>
        public virtual List<TEntity> Where(Expression<Func<TEntity, bool>> filter, params Expression<Func<TEntity, object>>[] includes)
        {
            DbSet<TEntity> dbSet = Context.Set<TEntity>();

            IQueryable<TEntity> query = dbSet;

            query = query.Where(filter);

            if (includes != null)
            {
                query = includes.Aggregate(query,
                          (current, include) => current.Include(include));
            }

            return query.ToList();
        }
    }
}