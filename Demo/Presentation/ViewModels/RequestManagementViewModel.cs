using CommonServiceLocator;
using Demo.Abstractions.AppServices;
using Demo.Abstractions.Common;
using Demo.Abstractions.Domain.Entities;
using Demo.Abstractions.Presentation.Services;
using Demo.Properties;
using Demo.Services;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Demo.Presentation.ViewModels
{
    public class RequestManagementViewModel : CollectionViewModel<IRequest, RequestViewModel, IRequestAppService>
    {
        protected override string SuccessfullyAddedMessage => Resources.SuccessfullyAddedRequest;

        private bool _showAccepted;
        private bool _showRejected;
        private bool _showPending;
        public bool ShowAccepted
        {
            get => _showAccepted;
            set
            {
                Set(ref _showAccepted, value);
                View.Refresh();
            }
        }
        public bool ShowRejected
        {
            get => _showRejected;
            set
            {
                Set(ref _showRejected, value);
                View.Refresh();
            }
        }
        public bool ShowPending
        {
            get => _showPending;
            set
            {
                Set(ref _showPending, value);
                View.Refresh();
            }
        }

        public ICommand RejectCurrentCommand { get; set; }
        public ICommand AcceptCurrentCommand { get; set; }

        public RequestManagementViewModel()
        {
            Load();
            RejectCurrentCommand = new RelayCommand(RejectCurrentAction);
            AcceptCurrentCommand = new RelayCommand(AcceptCurrentAction);
            ShowPending = true;
        }

        protected override void GetEntityReady(RequestViewModel vm)
        {
            vm.RequestDateTime = DateTime.Now;
        }
        protected override SortItem[] GetSortItems()
        {
            return new[]
            {
                new SortItem("Fecha", new SortDescription("RequestDateTime", ListSortDirection.Descending)),
                new SortItem("Nombre", new SortDescription("CompleteName", ListSortDirection.Ascending)),
                new SortItem("Estado", new SortDescription("State", ListSortDirection.Ascending)),
                new SortItem("Perfíl", new SortDescription("OccupationalProfile", ListSortDirection.Ascending)),
            };
        }
        protected override bool FilterPredicate(object obj)
        {
            if (obj is RequestViewModel vm)
            {
                if (ShowAccepted == ShowPending && ShowPending == ShowRejected)
                {
                    return true;
                }
                switch (vm.State)
                {
                    default:
                    case RequestState.Pending: return ShowPending;
                    case RequestState.Accepted: return ShowAccepted;
                    case RequestState.Rejected: return ShowRejected;
                }
            }
            return false;
        }
        protected override RequestViewModel GetEntityViewModel()
        {
            return new RequestViewModel();
        }
        protected override async void CollectionModified(IRequest original, IRequest modified, EntityOperation operation)
        {
            base.CollectionModified(original, modified, operation);
            if (operation == EntityOperation.Add)
            {
                await EmailNotificationService.SendEmailAsync(EmailTemplates.Received(modified), modified.Email);
                if (modified.Age > 65)
                {
                    RejectInternal(modified);
                }
            }
        }

        public override void CopyEntityValues(IRequest source, IRequest target)
        {
            target.Id = source.Id;
            target.FirstName = source.FirstName;
            target.LastName = source.LastName;
            target.Age = source.Age;
            target.OccupationalProfile = source.OccupationalProfile;
            target.Email = source.Email;
            target.HealthInsurance = source.HealthInsurance;
            target.State = source.State;
            target.RequestDateTime = source.RequestDateTime;
            target.Notes = source.Notes;
            target.WorkplaceId = source.WorkplaceId;
            target.FormNumber = source.FormNumber;
        }

        private async void RejectCurrentAction()
        {
            var request = Current;
            if (request == null)
                return;
            var msgSvc = ServiceLocator.Current.GetInstance<IMessageService>();
            //TODO: Resources:
            if (await msgSvc.ShowMessageAsync(Resources.RequestRejectPromptTitle, Resources.RequestRejectPromptContent))
            {
                RejectInternal(request);
            }
        }
        private async void AcceptCurrentAction()
        {
            if (Current == null)
                return;
            var dlgSvc = ServiceLocator.Current.GetInstance<IDialogService>();
            var selector = ServiceLocator.Current.GetInstance<WorkplaceSelectorViewModel>();
            selector.Refresh();
            if (await dlgSvc.ShowContentAsync(typeof(WorkplaceSelectorViewModel), selector))
            {
                var msgSvc = ServiceLocator.Current.GetInstance<IMessageService>();
                var workplace = selector.Selected;
                if (workplace != null)
                {
                    var request = Current;
                    request.WorkplaceId = workplace.Id;
                    request.State = RequestState.Accepted;
                    StoreItem(request.Id);
                    var wpManager = ServiceLocator.Current.GetInstance<WorkplaceManagementViewModel>();
                    workplace.Assigned = true;
                    wpManager.StoreItem(workplace.Id);
                    await msgSvc.ShowNotificationAsync(Resources.WorkplaceAssignedNotification, MessageType.Notification);

                    await EmailNotificationService.SendEmailAsync(EmailTemplates.Accepted(request.FirstName, workplace), request.Email);
                }
                else
                {
                    await msgSvc.ShowNotificationAsync(Resources.WorkplaceAssignedError, MessageType.Error);
                }
            }
            //TODO: Resources:
            //if (await msgSvc.ShowMessageAsync("Rechazar solicitud", "¿Seguro que quiere rechazar esta solicitud?"))

        }

        private async void RejectInternal(IRequest item) 
        {
            item.State = RequestState.Rejected;
            StoreItem(item.Id);
            await EmailNotificationService.SendEmailAsync(EmailTemplates.Rejected(item.FirstName), item.Email);
        }
    }
}
