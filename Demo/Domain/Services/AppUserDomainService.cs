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
    public class AppUserDomainService: DomainService<IAppUser, IAppUserRepository>, IAppUserDomainService
    {
        public AppUserDomainService(IAppUserRepository repository)
            :base(repository)
        {
        }
    }
}
