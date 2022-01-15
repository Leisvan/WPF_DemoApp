using CommonServiceLocator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Presentation.ViewModels
{
    public class ViewModelLocator
    {
        public HomeViewModel Home => ServiceLocator.Current.GetInstance<HomeViewModel>();
        public NavigationBarViewModel NavigationBar => ServiceLocator.Current.GetInstance<NavigationBarViewModel>();
        public RequestManagementViewModel RequestsCollection => ServiceLocator.Current.GetInstance<RequestManagementViewModel>();
        public AdministrationViewModel Administration => ServiceLocator.Current.GetInstance<AdministrationViewModel>();

    }
}
