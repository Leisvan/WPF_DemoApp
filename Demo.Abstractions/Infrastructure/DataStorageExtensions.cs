using Demo.Abstractions.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Abstractions.Infrastructure
{
    public static class DataStorageExtensions
    {
        public static void AddMany<TEntity>(this IDataStorage ds, IEnumerable<TEntity> collection) 
            where TEntity : class, IEntity
        {
            foreach (var item in collection)
            {
                ds.Add(item);
            }
        }
    }
}
