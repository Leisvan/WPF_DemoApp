using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Presentation.ViewModels
{
    public class MessageViewModel : ViewModelBase
    {
        private string _title;
        private string _message;

        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                RaisePropertyChanged();
            }
        }
        public string Message
        {
            get => _message;
            set
            {
                _message = value;
                RaisePropertyChanged();
            }
        }

        public MessageViewModel()
        {
        }

    }
}
