using Demo.Abstractions.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Domain.Entities
{
    public class Workplace : Entity, IWorkplace
    {
        public string CompanyName { get; set; }
        public string Occupation { get; set; }
        public string Notes { get; set; }
        public bool Assigned { get; set; }
    }
}
