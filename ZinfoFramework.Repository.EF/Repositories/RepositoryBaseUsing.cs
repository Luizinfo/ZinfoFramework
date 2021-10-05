using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using ZinfoFramework.Repository.EF.Contex;
using ZinfoFramework.Repository.Interfaces;

namespace ZinfoFramework.Repository.EF.Repositories
{
    public abstract class RepositoryBaseUsing<TEntity> : IRepositoryBase<TEntity>
        where TEntity : class
    {
        protected string connectionString;

        public RepositoryBaseUsing(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public abstract BaseContext GetContext();

        public void Add(TEntity entity)
        {
            using (BaseContext db = GetContext())
            {
                DbSet<TEntity> dbSet = db.Set<TEntity>();
                dbSet.Add(entity);
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Método que adiciona uma lista de novos objetos ao banco de dados da aplicação.
        /// </summary>
        public void AddAll(List<TEntity> entity)
        {
            using (BaseContext db = GetContext())
            {
                DbSet<TEntity> dbSet = db.Set<TEntity>();

                foreach (var item in entity)
                {
                    dbSet.Add(item);
                }
                db.SaveChanges();
            }
        }

        public void Delete(TEntity entity)
        {
            using (BaseContext db = GetContext())
            {
                DbSet<TEntity> dbSet = db.Set<TEntity>();

                if (db.Entry(entity).State == EntityState.Detached)
                {
                    dbSet.Attach(entity);
                }
                dbSet.Remove(entity);
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Método que deleta um ou varios objetos no banco de dados da aplicação, mediante uma expressão LINQ.
        /// </summary>
        public void DeleteAll(Expression<Func<TEntity, bool>> filter = null)
        {
            using (BaseContext db = GetContext())
            {
                DbSet<TEntity> dbSet = db.Set<TEntity>();

                IQueryable<TEntity> query = dbSet;
                List<TEntity> listDelete = query.Where(filter).ToList();

                foreach (var item in listDelete)
                {
                    dbSet.Remove(item);
                }
                db.SaveChanges();
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public void Edit(TEntity entity)
        {
            using (BaseContext db = GetContext())
            {
                DbSet<TEntity> dbSet = db.Set<TEntity>();

                dbSet.Update(entity);
                db.SaveChanges();
            }
        }

        public virtual List<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null)
        {
            using (BaseContext db = GetContext())
            {
                DbSet<TEntity> dbSet = db.Set<TEntity>();

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
        }

        public TEntity Get(long id)
        {
            using (BaseContext db = GetContext())
            {
                DbSet<TEntity> dbSet = db.Set<TEntity>();

                return dbSet.Find(id);
            }
        }

        public bool Existe(Expression<Func<TEntity, bool>> filter)
        {
            using (BaseContext db = GetContext())
            {
                DbSet<TEntity> dbSet = db.Set<TEntity>();

                return dbSet.AsNoTracking().Any(filter);
            }
        }

        /// <summary>
        /// Método que busca uma lista de objetos no banco de dados da aplicação e retorna-a no tipo IEnumerable<TEntity>.
        /// </summary>
        public virtual List<TEntity> Where(Expression<Func<TEntity, bool>> filter, params Expression<Func<TEntity, object>>[] includes)
        {
            using (BaseContext db = GetContext())
            {
                DbSet<TEntity> dbSet = db.Set<TEntity>();

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
}