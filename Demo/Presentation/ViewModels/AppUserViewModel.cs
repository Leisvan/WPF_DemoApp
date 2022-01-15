using Demo.Abstractions.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Presentation.ViewModels
{
    public class AppUserViewModel : EntityViewModel<IAppUser>, IAppUser
    {
        private string _username;
        private string _name;
        private string _password;
        private int _avatarId;
        private AppUserType _userType;

        public string Name
        {
            get => _name;
            set => Set(ref _name, value);
        }
        public string UserName
        {
            get => _username;
            set => Set(ref _username, value);
        }
        public string Password
        {
            get => _password;
            set => Set(ref _password, value);
        }
        public int AvatarId
        {
            get => _avatarId;
            set => Set(ref _avatarId, value);
        }
        public AppUserType UserType
        {
            get => _userType;
            set => Set(ref _userType, value);
        }

        public AppUserViewModel()
        {
            AvatarId = 1;
        }

        public override void CopyTo(IAppUser entity)
        {
            entity.Id = Id;
            entity.Name = Name;
            entity.UserName = UserName;
            entity.Password = Password;
            entity.UserType = UserType;
            entity.AvatarId = AvatarId;
        }
    }
}
