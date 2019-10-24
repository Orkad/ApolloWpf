using ApolloWpf.Business;
using ApolloWpf.ViewModel;
using ApolloWpfCore.Interfaces;
using GalaSoft.MvvmLight.Ioc;
using System.Threading;

namespace ApolloWpf.View
{
    public class ViewModelInstaller : IInstaller
    {
        public void Install()
        {
            new ServiceInstaller().Install();

            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<LoginViewModel>();
        }
    }
}
