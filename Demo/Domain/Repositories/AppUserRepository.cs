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
    public class AppUserRepository: GenericRepository<IAppUser, AppUser>, IAppUserRepository
    {
        public AppUserRepository(IDataStorage dataStorage): base(dataStorage)
        {
        }
    }
}