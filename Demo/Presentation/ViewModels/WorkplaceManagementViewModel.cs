using Demo.Abstractions.AppServices;
using Demo.Abstractions.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Presentation.ViewModels
{
    public class WorkplaceManagementViewModel : CollectionViewModel<IWorkplace, WorkplaceViewModel, IWorkplaceAppService>
    {
        public WorkplaceManagementViewModel()
        {
            Load();
        }
        
        public override void CopyEntityValues(IWorkplace source, IWorkplace target)
        {
            target.Id = source.Id;
            target.CompanyName = source.CompanyName;
            target.Occupation = source.Occupation;
            target.Notes = source.Notes;
            target.Assigned = source.Assigned;
        }

        protected override bool FilterPredicate(object obj)
        {
            if (obj is WorkplaceViewModel wp)
            {
                return !wp.Assigned;
            }
            return false;
        }
        protected override WorkplaceViewModel GetEntityViewModel()
        {
            return new WorkplaceViewModel();
        }
    }
}
