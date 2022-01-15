using CommonServiceLocator;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Presentation.ViewModels
{
    public class AdministrationViewModel: ViewModelBase
    {
        public AppUserManagementViewModel AppUserCollection => ServiceLocator.Current.GetInstance<AppUserManagementViewModel>();
        public WorkplaceManagementViewModel WorkplaceCollection => ServiceLocator.Current.GetInstance<WorkplaceManagementViewModel>();

    }
}
