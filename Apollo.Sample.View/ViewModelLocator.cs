using Apollo.MVVM.Commands;
using Apollo.Sample.Model.Business.Services;
using Apollo.Sample.View.Pages;
using Apollo.Sample.ViewModel;
using Apollo.WPF.Factories;
using Apollo.WPF.Services.Dialog;
using Apollo.WPF.Services.Navigation;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using System;
using System.Linq;
using System.Windows;
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
        /// Enregistre une définition
        /// </summary>
        /// <typeparam name="TClass">définition de l'instance</typeparam>
        static void Register<TClass>()
            where TClass : class
            => SimpleIoc.Default.Register<TClass>();

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
                return;
            }

            // View Models
            Register<MainViewModel>();
            Register<LoginViewModel>();
            Register<RegisterViewModel>();
            Register<IndexViewModel>();

            // Services
            Register<IAuthenticationService, AuthenticationService>();
            Register<IUserService, UserService>();

            // View Services (WPF)
            RegisterSingleton<INavigationService>(GetConfiguredNavigationService());
            RegisterSingleton<IDialogService>(new MetroDialogService());
            RegisterSingleton<ICommandFactory>(new WpfCommandFactory());
        }

        private static FrameNavigationService GetConfiguredNavigationService()
        {
            var nav = new FrameNavigationService();
            nav.Configure<IndexPage>("IndexPage");
            nav.Configure<LoginPage>("LoginPage");
            nav.Configure<RegisterPage>("RegisterPage");
            return nav;
        }

        public MainViewModel MainViewModel => Resolve<MainViewModel>();
        public LoginViewModel LoginViewModel => Resolve<LoginViewModel>();
        public RegisterViewModel RegisterViewModel => Resolve<RegisterViewModel>();
        public IndexViewModel IndexViewModel => Resolve<IndexViewModel>();
    }
}