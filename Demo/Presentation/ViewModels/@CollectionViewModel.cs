using CommonServiceLocator;
using Demo.Abstractions.AppServices;
using Demo.Abstractions.Common;
using Demo.Abstractions.Domain.Entities;
using Demo.Abstractions.Presentation.Services;
using Demo.Abstractions.Presentation.ViewModels;
using Demo.Properties;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace Demo.Presentation.ViewModels
{
    public abstract class CollectionViewModel<T, TViewModel, TAppService> :
            ObservableCollection<TViewModel>
            where T : class, IEntity
            where TViewModel : IEntityViewModel<T>, T
            where TAppService : IEntityManagerAppService<T>
    {
        private bool _isLoading;
        private string _searchText;

        public ICollectionView View { get; }
        public ICommand AddCommand { get; }
        public ICommand EditCurrentCommand { get; }
        public ICommand DeleteCurrentCommand { get; }
        public ICollectionView SortItemsView { get; private set; }

        public TViewModel Current => (TViewModel)View.CurrentItem;
        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                _isLoading = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(IsLoading)));
            }
        }
        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value.ToLowerInvariant();
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(SearchText)));
                View.Refresh();
                View.MoveCurrentToFirst();
            }
        }

        protected TAppService AppService => CreateAppService();


        protected virtual string SuccessfullyAddedMessage => Resources.SuccessfullyAddedElement;
        protected virtual string SuccessfullyUpdatedMessage => Resources.SuccessfullyUpdatedElement;
        protected virtual string SuccessfullyRemovedMessage => Resources.SuccessfullyRemoveElement;
        protected virtual string SureToRemoveMessage => Resources.SureToRemoveElement;

        protected IMessageService MessageService { get; } = ServiceLocator.Current.GetInstance<IMessageService>();
        protected IDialogService DialogService { get; } = ServiceLocator.Current.GetInstance<IDialogService>();
        protected INavigationService NavigationService { get; } = ServiceLocator.Current.GetInstance<INavigationService>();

        public CollectionViewModel()
        {
            View = CollectionViewSource.GetDefaultView(Items);
            AddCommand = new RelayCommand(AddAction, CanAddAction);
            EditCurrentCommand = new RelayCommand(EditCurrentAction, CanEditAction);
            DeleteCurrentCommand = new RelayCommand(DeleteCurrentAction, CanDeleteAction);
            View.Filter = FilterPredicate;
            View.CurrentChanged += CurrentChanged;
            InitializeSortCriteria();
        }

        public virtual void Load()
        {
            foreach (var entity in AppService.All)
            {
                TViewModel viewModel = GetEntityViewModel(entity);
                Add(viewModel);
            }
            Refresh();
        }
        public void Refresh()
        {
            View.Refresh();
            if (View.CurrentItem == null && Items.Count > 0)
            {
                View.MoveCurrentToFirst();
            }
        }
        public virtual void ClearFilters()
        {
            SearchText = string.Empty;
        }
        public virtual TViewModel FindById(int id)
        {
            if (id > 0)
            {
                var item = Items.FirstOrDefault((x) => x.Id == id);
                return item;
            }
            return default;
        }
        public virtual void StoreItem(int id)
        {
            var item = FindById(id);
            if (item != null)
            {
                var toSave = AppService.Create();
                item.CopyTo(toSave);
                AppService.Update(toSave);
                CollectionModified(item, item, EntityOperation.Edit);
                Refresh();
            }
        }
        public abstract void CopyEntityValues(T source, T target);

        protected virtual TAppService CreateAppService()
        {
            return ServiceLocator.Current.GetInstance<TAppService>();
        }
        protected abstract TViewModel GetEntityViewModel();
        protected virtual TViewModel GetEntityViewModel(T entity)
        {
            var vm = GetEntityViewModel();
            CopyEntityValues(entity, vm);
            return vm;
        }

        protected async void AddAction()
        {
            TViewModel vm = GetEntityViewModel();

            if (!await DialogService.ShowContentAsync(typeof(TViewModel), vm))
                return;

            GetEntityReady(vm);

            T entity = AppService.Create();
            vm.CopyTo(entity);

            AddViewModelFromEntity(entity, vm);

            await MessageService.ShowNotificationAsync(SuccessfullyAddedMessage, MessageType.Notification);
        }
        protected async void EditCurrentAction()
        {
            TViewModel original = Current;
            TViewModel editedData = GetEntityViewModel();
            original.CopyTo(editedData);

            if (!await DialogService.ShowContentAsync(typeof(TViewModel), editedData)) return;

            T toSave = AppService.Create();
            editedData.CopyTo(toSave);
            AppService.Update(toSave);
            //if (original is INotifiable notifiable)
            //{
            //    notifiable.Updated();
            //}
            CollectionModified(original, editedData, EntityOperation.Edit);
            editedData.CopyTo(original);
            Refresh();

            await MessageService.ShowNotificationAsync(SuccessfullyUpdatedMessage, MessageType.Notification);
        }
        protected async void DeleteCurrentAction()
        {
            TViewModel original = Current;

            if (!await MessageService.ShowMessageAsync(Resources.Warning, SureToRemoveMessage)) return;

            T entity = AppService.Create();
            DeleteEntity(entity, original);
            CollectionModified(original, entity, EntityOperation.Remove);

            await MessageService.ShowNotificationAsync(
                SuccessfullyRemovedMessage,
                MessageType.Notification,
                Resources.Undo, () =>
                {
                    AddViewModelFromEntity(entity, original);
                });
        }

        protected virtual bool CanAddAction()
        {
            return true;
        }
        protected virtual bool CanEditAction()
        {
            return Current != null;
        }
        protected virtual bool CanDeleteAction()
        {
            return Current != null;
        }
        protected virtual bool FilterPredicate(object obj)
        {
            return true;
        }
        protected virtual void OnCurrentChanged()
        {
        }
        protected virtual void CollectionModified(T original, T modified, EntityOperation operation)
        {

        }
        
        protected void UpdateSortDescriptions()
        {
            View.SortDescriptions.Clear();
            if (SortItemsView.CurrentItem is SortItem item)
            {
                View.SortDescriptions.Add(item.Description);
                View.Refresh();
            }
            
        }
       
        protected virtual SortItem[] GetSortItems()
        {
            return new[]
            {
                new SortItem("Nombre", new SortDescription("Name", ListSortDirection.Ascending))
            };
        }
        protected virtual void GetEntityReady(TViewModel vm)
        {

        }
        protected bool Set<TProp>(ref TProp field, TProp newValue = default, [CallerMemberName] string propertyName = null)
        {
            if (Equals(field, newValue) || ReferenceEquals(field, newValue))
                return false;

            field = newValue;
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
            return true;
        }

        protected virtual void AddViewModelFromEntity(T entity, TViewModel viewModel)
        {
            AppService.Add(entity);
            viewModel.Id = entity.Id;

            Add(viewModel);
            View.Refresh();
            View.MoveCurrentTo(viewModel);
            CollectionModified(entity, viewModel, EntityOperation.Add);
        }
        private void DeleteEntity(T entity, TViewModel viewModel)
        {
            viewModel.CopyTo(entity);
            AppService.Remove(entity);

            int index = View.CurrentPosition;
            Items.Remove(viewModel);
            Refresh();

            if (Items.Count <= 0) return;

            if (index >= Items.Count) index--;

            View.Refresh();
            View.MoveCurrentToPosition(index);
            //if (viewModel is INotifiable notifiable)
            //{
            //    notifiable.Detached();
            //}

        }
        private void CurrentChanged(object sender, EventArgs e)
        {
            ((RelayCommand)AddCommand).RaiseCanExecuteChanged();
            ((RelayCommand)EditCurrentCommand).RaiseCanExecuteChanged();
            ((RelayCommand)DeleteCurrentCommand).RaiseCanExecuteChanged();
            OnPropertyChanged(new PropertyChangedEventArgs(nameof(Current)));
            OnCurrentChanged();
        }

        private void InitializeSortCriteria()
        {
            List<SortItem> items = new List<SortItem>(GetSortItems());
            SortItemsView = new CollectionViewSource { Source = items }.View;
            SortItemsView.CurrentChanged += SortItemsViewCurrentChanged;
            SortItemsView.MoveCurrentToFirst();
        }
        private void SortItemsViewCurrentChanged(object sender, EventArgs e)
        {
            UpdateSortDescriptions();
        }

        protected class SortItem
        {
            public string DisplayName { get; set; }
            public SortDescription Description { get; set; }

            public SortItem(string displayName, SortDescription description)
            {
                DisplayName = displayName;
                Description = description;
            }

            public override string ToString()
            {
                return DisplayName;
            }
        }
    }
}
