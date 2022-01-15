using Demo.Abstractions.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Abstractions.Infrastructure
{
    public interface IDataStorage : IDisposable
    {
        long Count<TEntity>() where TEntity : class, IEntity;

        TEntity FindById<TEntity>(object id) where TEntity : class, IEntity;

        TEntity Add<TEntity>(TEntity entity) where TEntity : class, IEntity;

        void Update<TEntity>(TEntity entity) where TEntity : class, IEntity;

        void Remove<TEntity>(TEntity entity) where TEntity : class, IEntity;

        IQueryable<TEntity> GetAll<TEntity>() where TEntity : class, IEntity;

        void SaveChanges();

        void Rollback();

        IQueryable<TEntity> Where<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class, IEntity;
    }
}
