using CommonServiceLocator;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Presentation.ViewModels
{
    public class HomeViewModel: ViewModelBase
    {
        public RequestManagementViewModel Requests => ServiceLocator.Current.GetInstance<RequestManagementViewModel>();
    }
}
