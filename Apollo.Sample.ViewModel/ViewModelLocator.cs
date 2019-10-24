using ApolloWpf.ViewModel;
using GalaSoft.MvvmLight.Ioc;

namespace Apollo.Sample.ViewModel
{
    public class ViewModelLocator
    {
        public MainViewModel Main => SimpleIoc.Default.GetInstance<MainViewModel>();
        public LoginViewModel Login => SimpleIoc.Default.GetInstance<LoginViewModel>();
        public RegisterViewModel Register => SimpleIoc.Default.GetInstance<RegisterViewModel>();
    }
}