using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Abstractions.Domain.Entities
{
    public enum RequestState : byte
    {
        Pending = 0,
        Accepted = 2,
        Rejected = 4,
    }
}
