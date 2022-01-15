using CommonServiceLocator;
using Demo.Abstractions.Domain.Entities;
using Demo.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Presentation.ViewModels
{
    public class RequestViewModel : EntityViewModel<IRequest>, IRequest
    {
        private string _firstName;
        private string _lastName;
        private int _age;
        private string _occupationProf;
        private string _email;
        private string _healthInsurance;
        private RequestState _state;
        private DateTime _requestDate;
        private string _notes;
        private int _workplaceId;
        private string _formNumber;

        public string FirstName
        {
            get => _firstName;
            set => Set(ref _firstName, value);
        }
        public string LastName
        {
            get => _lastName;
            set => Set(ref _lastName, value);
        }
        public string CompleteName => $"{FirstName} {LastName}";
        public int Age
        {
            get => _age;
            set => Set(ref _age, value);
        }
        public string OccupationalProfile
        {
            get => _occupationProf;
            set => Set(ref _occupationProf, value);
        }
        public string Email
        {
            get => _email;
            set => Set(ref _email, value);
        }
        public string HealthInsurance
        {
            get => _healthInsurance;
            set => Set(ref _healthInsurance, value);
        }
        public RequestState State
        {
            get => _state;
            set => Set(ref _state, value);
        }
        public int WorkplaceId
        {
            get => _workplaceId;
            set
            {
                Set(ref _workplaceId, value);
                RaisePropertyChanged(nameof(Workplace));
            }
        }
        public DateTime RequestDateTime
        {
            get => _requestDate;
            set => Set(ref _requestDate, value);
        }
        public string Notes
        {
            get => _notes;
            set => Set(ref _notes, value);
        }
        public string FormNumber
        {
            get => _formNumber;
            set => Set(ref _formNumber, value);
        }

        public WorkplaceViewModel Workplace
        {
            get
            {
                if (WorkplaceId > 0)
                {
                    var workplaceManager = ServiceLocator.Current.GetInstance<WorkplaceManagementViewModel>();
                    var wp = workplaceManager.FindById(WorkplaceId);
                    if (wp != null)
                    {
                        return wp;
                    }
                }
                return null;
            }
        }

        public RequestViewModel()
        {
            FormNumber = RandomIdGenerator.Generate();
        }

        public override void CopyTo(IRequest entity)
        {
            entity.Id = Id;
            entity.FirstName = FirstName;
            entity.LastName = LastName;
            entity.Age = Age;
            entity.OccupationalProfile = OccupationalProfile;
            entity.Email = Email;
            entity.HealthInsurance = HealthInsurance;
            entity.State = State;
            entity.RequestDateTime = RequestDateTime;
            entity.Notes = Notes;
            entity.WorkplaceId = WorkplaceId;
            entity.FormNumber = FormNumber;
        }
    }
}
