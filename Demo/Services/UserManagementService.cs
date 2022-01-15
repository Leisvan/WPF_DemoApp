using CommonServiceLocator;
using Demo.Abstractions.Domain.Entities;
using Demo.AppServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Services
{
    public static class UserManagementService
    {
        public static int LoggedInUserId
        {
            get;
            private set;
        }

        public static IAppUser TryLogin(string userName, string password)
        {
            var userAppService = ServiceLocator.Current.GetInstance<AppUserAppService>();
            var user = userAppService.All.Where(x => x.UserName == userName && x.Password == password)
                .SingleOrDefault();
            if (user != null)
            {
                LoggedInUserId = user.Id;
                return user;
            }
            Logout();
            return null;
        }
        public static void Logout()
        {
            LoggedInUserId = 0;
        }
    }
}
