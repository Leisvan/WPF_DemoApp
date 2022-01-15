using CommonServiceLocator;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Demo.Presentation.ViewModels
{
    public class WorkplaceSelectorViewModel
    {
        private WorkplaceManagementViewModel _manager;
        public ICollectionView View { get; private set; }
        public WorkplaceViewModel Selected { get; set; }
        public WorkplaceSelectorViewModel(WorkplaceManagementViewModel manager)
        {
            _manager = manager;
            Load();
        }
        private void Load()
        {
            View = new CollectionViewSource { Source = _manager }.View;
            View.CurrentChanged += View_CurrentChanged;
            View.Filter = (x =>
            {
                if (x is WorkplaceViewModel wp)
                {
                    return !wp.Assigned;
                }
                return false;
            });
        }
        public void Refresh()
        {
            View.Refresh();
        }

        private void View_CurrentChanged(object sender, EventArgs e)
        {
            Selected = View.CurrentItem as WorkplaceViewModel;
        }
    }
}
