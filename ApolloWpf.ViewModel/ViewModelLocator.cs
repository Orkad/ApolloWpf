using ApolloWpf.ViewModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ApolloWpf.View
{
    public class ViewModelLocator
    {
        public MainViewModel Main => SimpleIoc.Default.GetInstance<MainViewModel>();
        public LoginViewModel Login => SimpleIoc.Default.GetInstance<LoginViewModel>();
    }
}
