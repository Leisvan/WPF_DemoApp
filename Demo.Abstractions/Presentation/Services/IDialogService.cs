using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Abstractions.Presentation.Services
{
    public interface IDialogService
    {
        void ConfigureDialogs();
        Task<bool> ShowContentAsync(Type contentType, object parameter);
    }
}
