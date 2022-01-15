using Demo.Abstractions.Domain.Entities;
using Demo.Abstractions.Domain.Repositories;
using Demo.Abstractions.Infrastructure;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Domain.Repositories
{
    public abstract class AbstractionRepository<TEntity> :
         IRepository<TEntity> where TEntity : class, IEntity
    {
        protected AbstractionRepository(IDataStorage dataStorage)
        {
            dataStorage.AssertIsNotNull(nameof(dataStorage));

            DataStorage = dataStorage;
        }

        public long Count => DataStorage.Count<TEntity>();

        protected IDataStorage DataStorage { get; }

        public virtual TEntity Add(TEntity entity)
        {
            entity.AssertIsNotNull(nameof(entity));

            return DataStorage.Add(entity);
        }

        public virtual void Remove(TEntity entity)
        {
            entity.AssertIsNotNull(nameof(entity));

            DataStorage.Remove(entity);
        }

        public virtual void Update(TEntity entity)
        {
            entity.AssertIsNotNull(nameof(entity));

            DataStorage.Update(entity);
        }

        public IEnumerator<TEntity> GetEnumerator()
        {
            return GetAll().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
       
        public TEntity FindById(int id)
        {
            return DataStorage.FindById<TEntity>(id);
        }
                
        protected virtual IEnumerable<TEntity> GetAll()
        {
            return DataStorage.GetAll<TEntity>().Where(Filter).ToArray();
        }
        
        protected virtual bool Filter(TEntity entity)
        {
            return true;
        }

        public void Clear()
        {
            var copy = this.ToList();
            foreach (var item in copy)
            {
                Remove(item);
            }
        }
    }
}
