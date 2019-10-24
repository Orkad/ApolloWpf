using Apollo.MVVM.IOC;
using Apollo.Sample.ViewModel;
using Apollo.WPF.Services.Navigation;
using ApolloWpf.View.Pages;
using ApolloWpf.View.Services;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using System.Threading;

namespace ApolloWpf.View
{
    public class ApplicationInstaller : IInstaller
    {
        public void Install()
        {
            new ViewModelInstaller().Install();
            SimpleIoc.Default.Register(GetConfiguredNavigationService);
            SimpleIoc.Default.Register(GetConfiguredDialogService);
            Thread.Sleep(3000);
        }

        private IDialogService GetConfiguredDialogService()
        {
            return new MetroDialogService();
        }

        private INavigationService GetConfiguredNavigationService()
        {
            var navigationService = new FrameNavigationService();
            navigationService.Configure<MainPage>("main");
            navigationService.Configure<LoginPage>("login");
            return navigationService;
        }
    }
}