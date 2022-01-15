using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Abstractions.Domain.Entities
{
    public interface IAppUser : IEntity
    {
        string Name { get; set; }
        string UserName { get; set; }
        string Password { get; set; }
        int AvatarId { get; set; }
        AppUserType UserType { get; set; }
    }
}
