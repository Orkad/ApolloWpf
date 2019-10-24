using Apollo.MVVM.IOC;
using Apollo.Sample.Model.Business;
using ApolloWpf.ViewModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using System;
using System.Linq;

namespace Apollo.Sample.ViewModel
{
    public class ViewModelInstaller : IInstaller
    {
        public void Install()
        {
            new ServiceInstaller().Install();

            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<LoginViewModel>();
            SimpleIoc.Default.Register<RegisterViewModel>();
        }
    }
}