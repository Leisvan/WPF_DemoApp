using Demo.Abstractions.AppServices;
using Demo.Abstractions.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Presentation.ViewModels
{
    public class AppUserManagementViewModel : CollectionViewModel<IAppUser, AppUserViewModel, IAppUserAppService>
    {

        public AppUserManagementViewModel()
        {
            Load();
        }
        protected override AppUserViewModel GetEntityViewModel()
        {
            return new AppUserViewModel();
        }

        public override void CopyEntityValues(IAppUser source, IAppUser target)
        {
            target.Id = source.Id;
            target.Name = source.Name;
            target.UserName = source.UserName;
            target.Password = source.Password;
            target.UserType = source.UserType;
            target.AvatarId = source.AvatarId;
        }

    }
}
