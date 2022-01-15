using Demo.Abstractions.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Abstractions.Domain.Repositories
{
    public interface IRepository<TEntity> : IEnumerable<TEntity> where TEntity : IEntity
    {
        long Count { get; }
        TEntity FindById(int id);
        TEntity Add(TEntity entity);
        void Update(TEntity entity);
        void Remove(TEntity entity);
        void Clear();
    }
}
