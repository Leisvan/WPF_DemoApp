using Demo.Abstractions.Domain.Entities;
using Demo.Abstractions.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Infrastructure
{
    public class InMemoryDataStorage : IDataStorage
    {
        private readonly IDictionary<Type, IList<object>> Sets;
        private static readonly IDictionary<Type, int> Ids = new Dictionary<Type, int>();

        public InMemoryDataStorage()
        {
            Sets = new Dictionary<Type, IList<object>>();
        }

        public virtual void Dispose()
        {
        }

        public long Count<TEntity>() where TEntity : class, IEntity => GetEntitySet<TEntity>().Count;

        public TEntity FindById<TEntity>(object id) where TEntity : class, IEntity
        {
            var set = GetEntitySet<TEntity>();
            return (from TEntity entity in set
                    where Equals(entity.Id, id)
                    select entity).SingleOrDefault();
        }
        public TEntity Find<TEntity>(int id) where TEntity : class, IEntity

        {
            return (from TEntity entity in GetEntitySet<TEntity>()
                    where Equals(entity.Id, id)
                    select entity).SingleOrDefault();
        }
        public TEntity Add<TEntity>(TEntity entity) where TEntity : class, IEntity
        {
            if (Equals(entity, null))
                throw new ArgumentNullException("entity");

            if (entity.Id <= 0)
                entity.Id = GetNextId<TEntity>();

            GetEntitySet<TEntity>().Add(entity);

            return entity;
        }

        public void AddMany<TEntity>(IEnumerable<TEntity> collection) where TEntity : class, IEntity
        {
            foreach (var item in collection)
            {
                Add(item);
            }
        }

        public void Update<TEntity>(TEntity entity) where TEntity : class, IEntity
        {
            var set = GetEntitySet<TEntity>();
            var item = set.Select(x => x as IEntity).FirstOrDefault(x => x.Id == entity.Id);
            int index = set.IndexOf(item);
            set[index] = entity;
        }

        public void Remove<TEntity>(TEntity entity) where TEntity : class, IEntity
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            TEntity dbEntity = FindById<TEntity>(entity.Id);
            GetEntitySet<TEntity>().Remove(dbEntity);
        }

        public IQueryable<TEntity> GetAll<TEntity>() where TEntity : class, IEntity
        {
            return GetEntitySet<TEntity>().Cast<TEntity>().AsQueryable();
        }

        public void SaveChanges()
        {
        }
        public void Rollback()
        {
        }

        public IQueryable<TEntity> Where<TEntity>(Expression<Func<TEntity, bool>> predicate)
            where TEntity : class, IEntity
        {
            return GetEntitySet<TEntity>().Cast<TEntity>().AsQueryable().Where(predicate);
        }

        private IList<object> GetEntitySet<TEntity>()
        {
            Type entityType = typeof(TEntity);

            if (Sets.TryGetValue(entityType, out var set))
                return set;

            return Sets[entityType] = new List<object>();
        }

        private int GetNextId<TEntity>()
        {
            Type entityType = typeof(TEntity);

            if (Ids.TryGetValue(entityType, out int lastId))
                return Ids[entityType] = lastId + 1;

            return Ids[entityType] = 1;
        }
        private int GetEntityLastId<TEntity>()
        {
            Type entityType = typeof(TEntity);

            if (Ids.TryGetValue(entityType, out int lastId))
                return Ids[entityType];

            return 1;
        }
    }
}
