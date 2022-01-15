﻿using System;
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
    /// Interaction logic for WorkplaceSelectorUserControl.xaml
    /// </summary>
    public partial class WorkplaceSelectorUserControl : UserControl
    {
        public WorkplaceSelectorUserControl()
        {
            InitializeComponent();
        }
        public WorkplaceSelectorUserControl(object parameter)
        {
            InitializeComponent();
            DataContext = parameter;
        }
    }
}