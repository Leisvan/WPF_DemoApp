using Demo.Abstractions.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Abstractions.Domain.Services
{
    public interface IDomainService
    {
        bool Validate(object entity);
        bool CanAdd(object entity);
        bool CanRemove(object entity);
        bool CanUpdate(object entity);
        object Create();
    }
    public interface IDomainService<TEntity> :
        IDomainService
        where TEntity : IEntity
    {
        bool Validate(TEntity entity);
        bool CanAdd(TEntity entity);
        bool CanRemove(TEntity entity);
        bool CanUpdate(TEntity entity);
        new TEntity Create();
    }
}
