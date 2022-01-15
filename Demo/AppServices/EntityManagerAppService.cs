using CommonServiceLocator;
using Demo.Abstractions.AppServices;
using Demo.Abstractions.Domain.Entities;
using Demo.Abstractions.Domain.Repositories;
using Demo.Abstractions.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.AppServices
{
    public abstract class EntityManagerAppServiceBase<TEntity, TRepository, TDomainService> :
            IEntityManagerAppService<TEntity>
            where TEntity : IEntity
            where TRepository : class, IRepository<TEntity>
            where TDomainService : class, IDomainService<TEntity>
    {
        ~EntityManagerAppServiceBase()
        {
            Dispose(false);
        }

        public virtual IEnumerable<TEntity> All => Repository.ToArray();
        protected virtual TRepository Repository => ServiceLocator.Current.GetInstance<TRepository>();
        protected virtual TDomainService DomainService => ServiceLocator.Current.GetInstance<TDomainService>();

        public EntityManagerAppServiceBase()
        {
        }
        public virtual string GetKeyForCall(string methodName, params object[] arguments)
        {
            if (methodName == null) throw new ArgumentNullException(nameof(methodName));
            if (arguments == null) throw new ArgumentNullException(nameof(arguments));
            if (methodName == string.Empty)
                throw new ArgumentException("String cannot be empty", nameof(methodName));

            var stringBuilder = new StringBuilder();
            foreach (object argument in arguments)
            {
                switch (argument)
                {
                    case null:
                        stringBuilder.Append("null");
                        break;
                    case IEntity entity:
                        stringBuilder.Append($"{entity.GetType().FullName}-Id{entity.Id}");
                        break;
                    default:
                        stringBuilder.Append(argument);
                        break;
                }

                stringBuilder.Append(", ");
            }

            string argumentsString = stringBuilder.ToString();
            if (argumentsString.EndsWith(", "))
                argumentsString = argumentsString.Substring(0, argumentsString.Length - 2);
            return $"{GetType().FullName}.{methodName}({argumentsString})";
        }
        public TEntity FindById(int id)
        {
            return Repository.FindById(id);
        }
        public virtual TEntity Add(TEntity entity)
        {
            entity.AssertIsNotNull(nameof(entity));

            return CanAdd(entity) ? Repository.Add(entity) : default(TEntity);
        }
        public virtual TEntity AddOrUpdate(TEntity entity)
        {
            entity.AssertIsNotNull(nameof(entity));
            var e = GetStoredItem(entity);
            if (e != null)
            {
                entity.Id = e.Id;
                Update(entity);
                return entity;
            }
            else
            {
                return Add(entity);
            }
        }
        public virtual TEntity GetStoredItem(TEntity entity)
        {
            return FindById(entity.Id);
        }
        public virtual bool Remove(TEntity entity)
        {
            entity.AssertIsNotNull(nameof(entity));
            bool result = DomainService.CanRemove(entity);
            if (result)
            {
                Repository.Remove(entity);
            }
            return result;
        }
        public virtual void Update(TEntity entity)
        {
            entity.AssertIsNotNull(nameof(entity));

            DomainService.Validate(entity);
            if (DomainService.CanUpdate(entity))
                Repository.Update(entity);
        }
        public virtual bool CheckChanges(TEntity entity)
        {
            return FindById(entity.Id) == null;
        }
        public virtual bool CanAdd(TEntity entity)
        {
            return DomainService.CanAdd(entity);
        }
        public virtual TEntity Create()
        {
            return DomainService.Create();
        }
        public bool CanRemove(TEntity entity)
        {
            return DomainService.CanRemove(entity);
        }
        public bool CanUpdate(TEntity entity)
        {
            return DomainService.CanUpdate(entity);
        }
        public void Validate(TEntity entity)
        {

        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
        }
        public IEnumerable<TEntity> SyncWith(IEnumerable<TEntity> entities)
        {
            var result = new List<TEntity>();
            var repo = Repository.ToList();
            var toDelete = repo.Where(x => !entities.Any(y => y.Id == x.Id));
            foreach (var item in toDelete)
            {
                Remove(item);
            }
            foreach (var entity in entities)
            {
                result.Add(AddOrUpdate(entity));
            }
            return result;
        }
    }
}
