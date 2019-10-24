using Apollo.MVVM.IOC;
using Apollo.Sample.View.Pages;
using Apollo.Sample.ViewModel;
using Apollo.WPF.Services.Navigation;
using ApolloWpf.View.Services;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using System;
using System.Linq;
using System.Windows.Controls;

namespace Apollo.Sample.View
{
    public class ApplicationInstaller : IInstaller
    {
        public void Install()
        {
            new ViewModelInstaller().Install();
            SimpleIoc.Default.Register(GetConfiguredNavigationService);
            SimpleIoc.Default.Register(GetConfiguredDialogService);
        }

        private IDialogService GetConfiguredDialogService()
        {
            return new MetroDialogService();
        }

        private INavigationService GetConfiguredNavigationService()
        {
            var navigationService = new FrameNavigationService();
            var pages = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(t => typeof(Page).IsAssignableFrom(t));
            foreach (var page in pages)
            {
                navigationService.Configure(page, page.Name);
            }
            return navigationService;
        }
    }
}