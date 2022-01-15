using Demo.Abstractions.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Domain.Entities
{
    public class AppUser : Entity, IAppUser
    {
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int AvatarId { get; set; }
        public AppUserType UserType { get; set; }
    }
}
