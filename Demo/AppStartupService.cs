using CommonServiceLocator;
using Demo.Abstractions.AppServices;
using Demo.Abstractions.Domain.Entities;
using Demo.Abstractions.Domain.Repositories;
using Demo.Abstractions.Domain.Services;
using Demo.Abstractions.Infrastructure;
using Demo.Abstractions.Presentation.Services;
using Demo.AppServices;
using Demo.Domain.Entities;
using Demo.Domain.Repositories;
using Demo.Domain.Services;
using Demo.Infrastructure;
using Demo.Presentation.ViewModels;
using Demo.Presentation.Views;
using Demo.Sample.Infrastructure;
using Microsoft.Extensions.Configuration;
using Prism.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;
using Unity.ServiceLocation;

namespace Demo
{
    public class AppStartupService
    {
        private const string Logfoldername = "Logs";
        private const string Logfilename = "Logs.txt";

        private ILogger _logger;
        private UnityContainer _container;
        private ShellWindow _shell;
        private IConfiguration _config;

        public void Run()
        {
            _container = new UnityContainer();
            _logger = new AggregateLogger();
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            InitializeConfiguration();

            InitializeContainer();
            SetupDependencies();
            SetupAndStartPresentation();
        }

        private void InitializeContainer()
        {
            _logger.Log("Setting up the dependency container.");

            var container = new UnityServiceLocator(_container);
            ServiceLocator.SetLocatorProvider(() => container);
        }
        private void SetupDependencies()
        {
            _logger.Log("Setting up dependencies resolutions.");
            _container.RegisterInstance(_logger);

            // Data services
            _container.RegisterInstance<IDataStorage>(new InMemoryDataStorage());
            //Demo purposes
            //TODO: Remove in production:
            DemoData.AddDemoData();

            _container.RegisterInstance(_config);
            //Domain Entities
            _container.RegisterType<IAppUser, AppUser>();
            _container.RegisterType<IRequest, Request>();
            _container.RegisterType<IWorkplace, Workplace>();

            //Repositories
            _container.RegisterType<IAppUserRepository, AppUserRepository>();
            _container.RegisterType<IRequestRepository, RequestRepository>();
            _container.RegisterType<IWorkplaceRepository, WorkplaceRepository>();

            //Domain Services
            _container.RegisterType<IAppUserDomainService, AppUserDomainService>();
            _container.RegisterType<IRequestDomainService, RequestDomainService>();
            _container.RegisterType<IWorkplaceDomainService, WorkplaceDomainService>();

            //Application Services
            _container.RegisterType<IAppUserAppService, AppUserAppService>();
            _container.RegisterType<IRequestAppService, RequestAppService>();
            _container.RegisterType<IWorkplaceAppService, WorkplaceAppService>();
        }
        private void SetupAndStartPresentation()
        {
            _logger.Log("Registering view models dependencies.");

            //ViewModels:
            _container.RegisterSingleton<HomeViewModel>();
            _container.RegisterSingleton<NavigationBarViewModel>();
            _container.RegisterSingleton<AppUserManagementViewModel>();
            _container.RegisterSingleton<RequestManagementViewModel>();
            _container.RegisterSingleton<WorkplaceManagementViewModel>();
            _container.RegisterSingleton<WorkplaceSelectorViewModel>();

            //Shell:
            _logger.Log("Registering ShellView dependencies and services.");
            _shell = new ShellWindow();
            _container.RegisterInstance<IDialogService>(_shell);
            _container.RegisterInstance<INavigationService>(_shell);
            _container.RegisterInstance<IMessageService>(_shell);

            _shell.Initialize();

            System.Windows.Application.Current.MainWindow = _shell;
            _shell.ConfigureNavigation();
            _shell.ConfigureDialogs();
            _shell.ConfigureMessages();
            _shell.NavigateTo(typeof(HomeViewModel), null);
            _shell.Show();

        }
        private void InitializeConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, true);

            _config = builder.Build();
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
        }
    }
}
