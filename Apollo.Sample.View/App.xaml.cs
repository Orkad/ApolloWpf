using Apollo.MVVM.Security;
using Apollo.Sample.View.Windows;
using ApolloWpf.ViewModel;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Threading;
using GalaSoft.MvvmLight.Views;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Apollo.Sample.View
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected async override void OnStartup(StartupEventArgs e)
        {
            DispatcherHelper.Initialize();
            base.OnStartup(e);
            Thread.CurrentPrincipal = new SimplePrincipal();
            await Task.Run(() =>
            {
                new ApplicationInstaller().Install();
            });
            ShutdownMode = ShutdownMode.OnExplicitShutdown;
            MainWindow.Close();
            MainWindow = new MainWindow();
            MainWindow.Show();
            ShutdownMode = ShutdownMode.OnMainWindowClose;
        }
    }
}