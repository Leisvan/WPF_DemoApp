using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Abstractions.Presentation.Services
{
    public interface INavigationService
    {
        void ConfigureNavigation();
        void NavigateTo(Type destinationType, object parameter);
    }
}
