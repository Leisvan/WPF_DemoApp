using CommonServiceLocator;
using Demo.Abstractions.Domain.Entities;
using Demo.Abstractions.Domain.Repositories;
using Demo.Abstractions.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Domain.Services
{
    public abstract class DomainService<TEntity, TRepository> :
            IDomainService<TEntity>
            where TEntity : IEntity
            where TRepository : IRepository<TEntity>
    {
        protected DomainService(TRepository repository)
        {
            repository.AssertIsNotNull(nameof(repository));
            Repository = repository;
        }
        protected TRepository Repository { get; }
        public bool Validate(TEntity entity)
        {
            return true;
        }
        bool IDomainService.Validate(object entity)
        {
            return Validate((TEntity)entity);
        }
        public virtual bool CanAdd(TEntity entity)
        {
            return true;
        }
        bool IDomainService.CanAdd(object entity)
        {
            return CanAdd((TEntity)entity);
        }
        public virtual bool CanRemove(TEntity entity)
        {
            return typeof(TEntity).IsValueType || entity != null;
        }
        bool IDomainService.CanRemove(object entity)
        {
            return CanRemove((TEntity)entity);
        }
        public virtual bool CanUpdate(TEntity entity)
        {
            return true;
        }

        bool IDomainService.CanUpdate(object entity)
        {
            return CanUpdate((TEntity)entity);
        }
        public virtual TEntity Create()
        {
            return ServiceLocator.Current.GetInstance<TEntity>();
        }
        object IDomainService.Create()
        {
            return Create();
        }
        protected virtual bool ValidateEntityInBusiness(TEntity entity)
        {
            return true;
        }
    }
}
