using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Abstractions.Domain.Entities
{
    public interface IRequest: IEntity
    {
        string FirstName { get; set; }
        string LastName { get; set; }
        int Age { get; set; }
        string OccupationalProfile { get; set; }
        string Email { get; set; }
        string HealthInsurance { get; set; }
        RequestState State { get; set; }
        DateTime RequestDateTime { get; set; }
        string Notes { get; set; }

        //Dependencies:
        int WorkplaceId { get; set; }
        string FormNumber { get; set; }
    }
}
