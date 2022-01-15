using Demo.Abstractions.Domain.Entities;
using Demo.Abstractions.Domain.Repositories;
using Demo.Abstractions.Infrastructure;
using Demo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Domain.Repositories
{
    public class WorkplaceRepository : GenericRepository<IWorkplace, Workplace>, IWorkplaceRepository
    {
        public WorkplaceRepository(IDataStorage dataStorage) : base(dataStorage)
        {
        }
    }
}
