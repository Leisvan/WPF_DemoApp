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
    public class RequestDomainService: DomainService<IRequest, IRequestRepository>, IRequestDomainService
    {
        public RequestDomainService(IRequestRepository repository)
            :base(repository)
        {

        }
    }
}
