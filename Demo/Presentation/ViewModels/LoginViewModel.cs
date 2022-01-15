using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Presentation.ViewModels
{
    public class LoginViewModel: ViewModelBase
    {
        private string _userName;
        private string _password;

        public string UserName 
        {
            get => _userName;
            set => Set(ref _userName, value);
        }
        public string Password
        {
            get => _password;
            set => Set(ref _password, value);
        }

    }
}
