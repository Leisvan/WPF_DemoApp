using Demo.Abstractions.Domain.Entities;
using Demo.Abstractions.Domain.Repositories;
using Demo.Abstractions.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Domain.Repositories
{
    public abstract class GenericRepository<TEntity, TConcreteEntity> :
        AbstractionRepository<TConcreteEntity>,
        IRepository<TEntity>
        where TEntity : class, IEntity
        where TConcreteEntity : class, TEntity
    {
        protected GenericRepository(IDataStorage dataStorage)
            : base(dataStorage)
        {
        }
        public TEntity Add(TEntity entity)
        {
            if (entity is TConcreteEntity concreteEntity)
                return ((IRepository<TConcreteEntity>)this).Add(concreteEntity);

            throw new InvalidOperationException($"Cannot add entity which type is different than: {typeof(TConcreteEntity).FullName} using the current repository.");
        }
        public virtual void Remove(TEntity entity)
        {
            if (entity is TConcreteEntity concreteEntity)
                ((IRepository<TConcreteEntity>)this).Remove(concreteEntity);
            else
            {
                throw new InvalidOperationException($"Cannot remove entity which type is different than: {typeof(TConcreteEntity).FullName} using the current repository.");
            }
        }
        public virtual void Update(TEntity entity)
        {
            if (entity is TConcreteEntity concreteEntity)
                base.Update(concreteEntity);
            else
            {
                throw new InvalidOperationException($"Cannot update entity which type is different than: {typeof(TConcreteEntity).FullName} using the current repository.");
            }
        }
        TEntity IRepository<TEntity>.FindById(int id)
        {
            return FindById(id);
        }
        IEnumerator<TEntity> IEnumerable<TEntity>.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
