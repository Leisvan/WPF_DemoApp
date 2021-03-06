using Demo.Abstractions.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Abstractions.AppServices
{
    public interface IRequestAppService: IEntityManagerAppService<IRequest>
    {
    }
}
