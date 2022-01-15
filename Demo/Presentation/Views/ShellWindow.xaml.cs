using CommonServiceLocator;
using Demo.Abstractions.Presentation;
using Demo.Abstractions.Presentation.Services;
using Demo.Presentation.ViewModels;
using GalaSoft.MvvmLight.Command;
using MaterialDesignThemes.Wpf;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Demo.Presentation.Views
{
    /// <summary>
    /// Interaction logic for ShellWindow.xaml
    /// </summary>
    public partial class ShellWindow : Window,
        INavigationService, IDialogService, IMessageService
    {
        public ShellWindow()
        {
        }

        public void Initialize()
        {
            InitializeComponent();
        }

        #region INavigationService
        private delegate ContentControl ViewProvider(object parameter);
        private Dictionary<string, ViewProvider> _navigationDict;

        public void ConfigureNavigation()
        {
            _navigationDict = new Dictionary<string, ViewProvider>
            {
                { GetNavigationKey(typeof(HomeViewModel)), (object parameter) => new HomeUserControl() },
                { GetNavigationKey(typeof(RequestManagementViewModel)), (object parameter) => new RequestsManagementUserControl() },
                { GetNavigationKey(typeof(AdministrationViewModel)), (object parameter) => new AdministrationUserControl() },
            };
           
            
        }

        public void NavigateTo(Type destinationType, object parameter)
        {
            string key = GetNavigationKey(destinationType);
            if (!_navigationDict.ContainsKey(key))
            {
                throw new InvalidOperationException("Can't navigate to that destination");
            }
            var c = _navigationDict[key](parameter);
            content.Content = c;
            if (c is INavigationTarget target)
            {
                ServiceLocator.Current.GetInstance<NavigationBarViewModel>()
                    .Header = target.Header;
            }
            BeginStoryboard(Resources["ContentChangingStorybard"] as Storyboard);
        }

        private static string GetNavigationKey(Type type)
        {
            return type.FullName;
        }
        #endregion

        #region IDialogService
        private Dictionary<string, ViewProvider> _contentDict;

        public void ConfigureDialogs()
        {
            _contentDict = new Dictionary<string, ViewProvider>()
            {
                { GetNavigationKey(typeof(RequestViewModel)), (object parameter) => new RequestEditUserControl(parameter) },
                { GetNavigationKey(typeof(AppUserViewModel)), (object parameter) => new AppUserEditUserControl(parameter) },
                { GetNavigationKey(typeof(WorkplaceViewModel)), (object parameter) => new WorkplaceEditUserControl(parameter) },
                { GetNavigationKey(typeof(LoginViewModel)), (object parameter) => new LoginUserControl(parameter) },
                { GetNavigationKey(typeof(WorkplaceSelectorViewModel)), (object parameter) => new WorkplaceSelectorUserControl(parameter) },

            };
        }
        public async Task<bool> ShowContentAsync(Type contentType, object parameter)
        {
            string key = GetNavigationKey(contentType);
            if (!_contentDict.ContainsKey(key))
            {
                //No content to show
                return false;
            }
            var content = _contentDict[key](parameter);
            ContentDialogUserControl dialog = new ContentDialogUserControl(content);
            return (bool)await DialogHost.Show(dialog, "RootDialog");
        }
        #endregion

        #region IMessageService
        public void ConfigureMessages()
        {
            _notificationTimer = new DispatcherTimer()
            {
                Interval = TimeSpan.FromSeconds(4),
            };
            _notificationTimer.Tick += NotificationTick;
        }
        private DispatcherTimer _notificationTimer;
        public async Task ShowNotificationAsync(string message, MessageType type, string actionContent, Action action)
        {
            messageSnackBar.Background = GetVisualBrushForMessage(type);
            SnackbarMessage sbMessage = (actionContent != null && action != null)
                ? new SnackbarMessage
                {
                    Content = message,
                    ActionContent = actionContent,
                    ActionCommand = new RelayCommand(() =>
                    {
                        UpdateNotification(null);
                        action.Invoke();
                    }, true),
                }
                : new SnackbarMessage()
                {
                    Content = message,
                };
            UpdateNotification(sbMessage);
            await Task.CompletedTask;
        }
        public async Task<bool> ShowMessageAsync(string title, string message)
        {
            var vm = new MessageViewModel { Title = title, Message = message };
            var view = new MessageDialogUserControl(vm);
            return (bool)await DialogHost.Show(view, "RootDialog");
        }
        private void UpdateNotification(SnackbarMessage sbMessage)
        {
            if (sbMessage == null)
            {
                _notificationTimer.Stop();
                messageSnackBar.IsActive = false;
            }
            else
            {
                if (_notificationTimer.IsEnabled)
                {
                    _notificationTimer.Stop();
                    messageSnackBar.IsActive = false;
                }
                _notificationTimer.Start();
                messageSnackBar.Message = sbMessage;
                messageSnackBar.IsActive = true;
            }
        }

        private static readonly Brush NotificationBrush = new SolidColorBrush(Color.FromRgb(0, 166, 81));
        private static readonly Brush WarningBrush = new SolidColorBrush(Color.FromRgb(249, 168, 37));
        private static readonly Brush ErrorBrush = new SolidColorBrush(Color.FromRgb(197, 40, 40));
        private static Brush GetVisualBrushForMessage(MessageType type)
        {
            switch (type)
            {
                default:
                case MessageType.Notification: return NotificationBrush;
                case MessageType.Warning: return WarningBrush;
                case MessageType.Error: return ErrorBrush;
            }
        }
        private void NotificationTick(object sender, EventArgs e)
        {
            UpdateNotification(null);
        }

        #endregion
    }
}
