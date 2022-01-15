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
    /// Interaction logic for WorkplaceEditUserControl.xaml
    /// </summary>
    public partial class WorkplaceEditUserControl : UserControl
    {
        public WorkplaceEditUserControl()
        {
            InitializeComponent();
        }

        public WorkplaceEditUserControl(object argument)
        {
            InitializeComponent();
            DataContext = argument;
        }
    }
}
