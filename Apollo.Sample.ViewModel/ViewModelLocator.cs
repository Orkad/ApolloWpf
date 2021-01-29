using Apollo.Sample.Model.Business.Services;
using Apollo.Sample.ViewModel.Designs;
using Apollo.Sample.ViewModel.Runtimes;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;

namespace Apollo.Sample.ViewModel
{
    public class ViewModelLocator
    {
        static ViewModelLocator()
        {
            if (ViewModelBase.IsInDesignModeStatic)
            {
                SimpleIoc.Default.Register<IMainViewModel, MainDesignViewModel>();
                SimpleIoc.Default.Register<ILoginViewModel, LoginDesignViewModel>();
                SimpleIoc.Default.Register<IRegisterViewModel, RegisterDesignViewModel>();
                SimpleIoc.Default.Register<IIndexViewModel, IndexDesignViewModel>();
                return;
            }

            // run times View Models
            SimpleIoc.Default.Register<IMainViewModel, MainViewModel>();
            SimpleIoc.Default.Register<ILoginViewModel, LoginViewModel>();
            SimpleIoc.Default.Register<IRegisterViewModel, RegisterViewModel>();
            SimpleIoc.Default.Register<IIndexViewModel, IndexViewModel>();

            // services
            SimpleIoc.Default.Register<IAuthenticationService, AuthenticationService>();
            SimpleIoc.Default.Register<IUserService, UserService>();
        }

        public IMainViewModel MainViewModel => Resolve<MainViewModel>();
        public ILoginViewModel LoginViewModel => Resolve<LoginViewModel>();
        public IRegisterViewModel RegisterViewModel => Resolve<RegisterViewModel>();
        public IIndexViewModel IndexViewModel => Resolve<IndexViewModel>();

        private static T Resolve<T>() => SimpleIoc.Default.GetInstance<T>();
    }
}