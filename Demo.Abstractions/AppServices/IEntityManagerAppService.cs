using Demo.Abstractions.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Abstractions.AppServices
{
    public interface IEntityManagerAppService<TEntity> : IDisposable
            where TEntity : IEntity
    {
        IEnumerable<TEntity> All { get; }
        TEntity FindById(int id);
        TEntity Add(TEntity entity);
        TEntity AddOrUpdate(TEntity entity);
        TEntity GetStoredItem(TEntity entity);
        void Update(TEntity entity);
        bool CheckChanges(TEntity entity);
        bool Remove(TEntity entity);
        TEntity Create();
        bool CanAdd(TEntity entity);
        bool CanRemove(TEntity entity);
        bool CanUpdate(TEntity entity);
        void Validate(TEntity entity);
        IEnumerable<TEntity> SyncWith(IEnumerable<TEntity> entities);
    }
}
