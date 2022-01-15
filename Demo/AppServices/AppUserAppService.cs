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
    public class AppUserAppService: EntityManagerAppServiceBase<IAppUser, IAppUserRepository, IAppUserDomainService>, IAppUserAppService
    {
    }
}
