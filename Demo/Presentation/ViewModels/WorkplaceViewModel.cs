using Demo.Abstractions.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Presentation.ViewModels
{
    public class WorkplaceViewModel : EntityViewModel<IWorkplace>, IWorkplace
    {
        private string _companyName;
        private string _occupation;
        private string _notes;
        private bool _assigned;

        public string CompanyName
        {
            get => _companyName;
            set => Set(ref _companyName, value);
        }
        public string Occupation
        {
            get => _occupation;
            set => Set(ref _occupation, value);
        }
        public string Notes
        {
            get => _notes;
            set => Set(ref _notes, value);
        }
        public bool Assigned
        {
            get => _assigned;
            set => Set(ref _assigned, value);
        }

        public override void CopyTo(IWorkplace entity)
        {
            entity.Id = Id;
            entity.CompanyName = CompanyName;
            entity.Occupation = Occupation;
            entity.Notes = Notes;
            entity.Assigned = Assigned;
        }
    }
}
