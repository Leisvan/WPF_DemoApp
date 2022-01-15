using Demo.Abstractions.Presentation.Services;
using GalaSoft.MvvmLight.Command;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for ContentDialogUserControl.xaml
    /// </summary>
    public partial class ContentDialogUserControl : UserControl
    {
        public ContentDialogUserControl()
        {
            InitializeComponent();

            AcceptCommand = DialogHost.CloseDialogCommand;
        }
        public ContentDialogUserControl(object content)
            : this()
        {
            InitializeComponent();
            container.Content = content;

            AcceptCommand = new RelayCommand<bool>(Accept, CanAccept);
        }

        public ICommand AcceptCommand { get; }
        private bool CanAccept(bool argument)
        {
            if (!argument) return true;

            //if (container.Content is IValidationControl vui)
            //{
            //    if (!vui.IsEntityValid)
            //    {
            //        return false;
            //    }
            //    vui.PostValidations();
            //}
            if (container.Content is FrameworkElement element &&
                element.DataContext is INotifyDataErrorInfo entity)
                return !entity.HasErrors;
            return true;
        }
        private void Accept(bool argument)
        {
            DialogHost.CloseDialogCommand.Execute(argument, this);
        }
    }
}
