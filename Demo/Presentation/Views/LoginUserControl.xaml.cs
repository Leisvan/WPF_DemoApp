using Demo.Presentation.ViewModels;
using Demo.Services;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for LoginUserControl.xaml
    /// </summary>
    public partial class LoginUserControl : UserControl
    {
        public LoginUserControl()
        {
            InitializeComponent();
        }
        public LoginUserControl(object argument)
        {
            InitializeComponent();
            DataContext = argument;
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            var pass = passBox.Password;
            if (DataContext is LoginViewModel vm && !string.IsNullOrEmpty(pass))
            {
                vm.Password = EncryptionService.Encrypt(pass);
            }
        }
    }
}
