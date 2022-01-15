using Demo.Abstractions.Presentation.Services;
using Demo.Presentation.ViewModels;
using Demo.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Demo.Presentation.Views
{
    /// <summary>
    /// Interaction logic for AppUserEditUserControl.xaml
    /// </summary>
    public partial class AppUserEditUserControl : UserControl, IValidationControl
    {
        public ObservableCollection<int> AvatarIds { get; set; } = new ObservableCollection<int>();

        public bool IsEntityValid
        {
            get
            {
                if (DataContext is AppUserViewModel appUser)
                {
                    return !string.IsNullOrEmpty(appUser.Name)
                        && !string.IsNullOrEmpty(appUser.UserName)
                        && !string.IsNullOrEmpty(appUser.Password);
                }
                return true;
            }
        }
        public void PostValidations()
        {
            if (DataContext is AppUserViewModel appUser)
            {
                appUser.Password = EncryptionService.Encrypt(appUser.Password);
            }
        }

        public AppUserEditUserControl()
        {
            InitializeComponent();
            AvatarIds.AddRange(AvatarProvider.GetAllIds());
        }
        public AppUserEditUserControl(object argument)
        {
            InitializeComponent();
            DataContext = argument;
            AvatarIds.AddRange(AvatarProvider.GetAllIds());
        }

        private void PasswordChanged(object sender, RoutedEventArgs e)
        {
            if ((DataContext is AppUserViewModel vm) && (sender is PasswordBox pbox))
            {
                var password = pbox.Password;
                vm.Password = EncryptionService.Encrypt(password);
            }
        }
    }
}
