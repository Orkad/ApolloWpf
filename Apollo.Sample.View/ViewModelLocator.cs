using Apollo.MVVM;
using Apollo.Sample.Model.Business.Services;
using Apollo.Sample.ViewModel;
using Apollo.Sample.ViewModel.Designs;
using Apollo.Sample.ViewModel.Runtimes;
using Apollo.WPF.Services.Dialog;
using Apollo.WPF.Services.Navigation;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using System;
using System.Linq;
using System.Windows.Controls;

namespace Apollo.Sample.View
{
    public class ViewModelLocator
    {
        #region Définition de l'injection

        /// <summary>
        /// Résoud l'instance précisée par le type donné
        /// </summary>
        /// <typeparam name="T">type souhaité</typeparam>
        public static T Resolve<T>() => SimpleIoc.Default.GetInstance<T>();

        /// <summary>
        /// Enregistre une implémentation d'interface
        /// </summary>
        /// <typeparam name="TInterface">type de l'interface</typeparam>
        /// <typeparam name="TClass">type de l'implémentation</typeparam>
        static void Register<TInterface, TClass>()
            where TInterface : class
            where TClass : class, TInterface
            => SimpleIoc.Default.Register<TInterface, TClass>();

        /// <summary>
        /// Enregistre une instance en tant que singleton pour l'interface donnée
        /// </summary>
        /// <typeparam name="TInterface">interface donné</typeparam>
        /// <param name="instance">instance qui sera toujours retournée</param>
        static void RegisterSingleton<TInterface>(TInterface instance)
            where TInterface : class
            => SimpleIoc.Default.Register(() => instance);

        #endregion

        static ViewModelLocator()
        {
            if (ViewModelBase.IsInDesignModeStatic)
            {
                Register<IMainViewModel, MainDesignViewModel>();
                Register<ILoginViewModel, LoginDesignViewModel>();
                Register<IRegisterViewModel, RegisterDesignViewModel>();
                Register<IIndexViewModel, IndexDesignViewModel>();
                return;
            }

            // View Models
            Register<IMainViewModel, MainViewModel>();
            Register<ILoginViewModel, LoginViewModel>();
            Register<IRegisterViewModel, RegisterViewModel>();
            Register<IIndexViewModel, IndexViewModel>();

            // Services
            Register<IAuthenticationService, AuthenticationService>();
            Register<IUserService, UserService>();

            // View Services
            RegisterSingleton<INavigationService>(GetConfiguredNavigationService());
            RegisterSingleton<IDialogService>(new MetroDialogService());
        }

        private static FrameNavigationService GetConfiguredNavigationService()
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

        public IMainViewModel MainViewModel => Resolve<IMainViewModel>();
        public ILoginViewModel LoginViewModel => Resolve<ILoginViewModel>();
        public IRegisterViewModel RegisterViewModel => Resolve<IRegisterViewModel>();
        public IIndexViewModel IndexViewModel => Resolve<IIndexViewModel>();
    }
}