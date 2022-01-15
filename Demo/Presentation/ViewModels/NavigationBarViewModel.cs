using CommonServiceLocator;
using Demo.Abstractions.Domain.Entities;
using Demo.Abstractions.Presentation.Services;
using Demo.Properties;
using Demo.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Demo.Presentation.ViewModels
{
    public class NavigationBarViewModel : ViewModelBase
    {
        private string _header;
        public string Header
        {
            get => _header;
            set => Set(ref _header, value);
        }

        public ICommand GoToRequestsCommand => new RelayCommand(() =>
        {
            NavigationService.NavigateTo(typeof(RequestManagementViewModel), null);
        });
        public ICommand GoToAdministrationCommand => new RelayCommand(() =>
        {
            NavigationService.NavigateTo(typeof(AdministrationViewModel), null);
        });

        public ICommand LoginCommand { get; private set; }
        public ICommand LogoutCommand { get; private set; }

        private AppUserViewModel _loggedInUser;
        public AppUserViewModel LoggedInUser
        {
            get => _loggedInUser;
            private set
            {
                Set(ref _loggedInUser, value);
            }
        }
        public bool IsUserLoggedIn { get => LoggedInUser != null; }

        public INavigationService NavigationService = ServiceLocator.Current.GetInstance<INavigationService>();

        public NavigationBarViewModel()
        {
            LoginCommand = new RelayCommand(LoginAction);
            LogoutCommand = new RelayCommand(LogoutAction);
        }

        private async void LoginAction()
        {
            var dlgServ = ServiceLocator.Current.GetInstance<IDialogService>();
            LoginViewModel vm = new LoginViewModel();
            if(await dlgServ.ShowContentAsync(typeof(LoginViewModel), vm))
            {
                var msgServ = ServiceLocator.Current.GetInstance<IMessageService>();
                var user = UserManagementService.TryLogin(vm.UserName, vm.Password);
                if (user != null)
                {
                    string firstName = user.Name.Split(' ')[0];
                    await msgServ.ShowNotificationAsync(string.Format(Resources.LoginWelcomeFormat, firstName), MessageType.Notification);
                    UpdateLoggedInUser();
                }
                else
                {
                    await msgServ.ShowNotificationAsync(Resources.LoginError, MessageType.Error);
                }
            }

        }
        private async void LogoutAction()
        {
            var msgServ = ServiceLocator.Current.GetInstance<IMessageService>();
            if (await msgServ.ShowMessageAsync(Resources.LogoutPromptTitle, Resources.LogoutPromptContent))
            {
                UserManagementService.Logout();
                LoggedInUser = null;
                NavigationService.NavigateTo(typeof(HomeViewModel), null);

                await msgServ.ShowNotificationAsync(Resources.LogoutNotification, MessageType.Notification);
                UpdateLoggedInUser();
            }
        }
        private void UpdateLoggedInUser()
        {
            var userId = UserManagementService.LoggedInUserId;
            if (userId > 0)
            {
                var userVM = ServiceLocator.Current.GetInstance<AppUserManagementViewModel>()
                    .FindById(userId);
                LoggedInUser = userVM;
                if (userVM.UserType == AppUserType.Admin)
                {
                    NavigationService.NavigateTo(typeof(AdministrationViewModel), null);
                }
                else if (userVM.UserType == AppUserType.Specialist)
                {
                    NavigationService.NavigateTo(typeof(RequestManagementViewModel), null);
                }
            }
            else
            {
                LoggedInUser = null;
                NavigationService.NavigateTo(typeof(HomeViewModel), null);
            }
            RaisePropertyChanged(nameof(IsUserLoggedIn));
        }


    }
}
