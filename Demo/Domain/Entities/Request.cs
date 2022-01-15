using Demo.Abstractions.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Domain.Entities
{
    public class Request : Entity, IRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string OccupationalProfile { get; set; }
        public string Email { get; set; }
        public string HealthInsurance { get; set; }
        public RequestState State { get; set; }
        public int WorkplaceId { get; set; }
        public DateTime RequestDateTime { get; set; }
        public string Notes { get; set; }
        public string FormNumber { get; set; }
    }
}
