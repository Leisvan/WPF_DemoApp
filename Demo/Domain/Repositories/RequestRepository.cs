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
    public class RequestRepository : GenericRepository<IRequest, Request>, IRequestRepository
    {
        public RequestRepository(IDataStorage dataStorage) : base(dataStorage)
        {
        }
    }
}
