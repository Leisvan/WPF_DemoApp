using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Abstractions.Domain.Entities
{
    public interface IWorkplace : IEntity
    {
        string CompanyName { get; set; }
        string Occupation { get; set; }
        string Notes { get; set; }
        bool Assigned { get; set; }
    }
}
