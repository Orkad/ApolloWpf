using Apollo.MVVM.Navigation;
using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Apollo.WPF.Services.Navigation
{
    public class PageNavigationService : INavigationService
    {
        /// <summary>
        /// Association d'une clef string avec un Type de Page
        /// </summary>
        private readonly Dictionary<string, Type> _pages = new Dictionary<string, Type>();

        /// <summary>
        /// Historique de navigation
        /// </summary>
        private readonly Stack<Page> _history = new Stack<Page>();

        /// <summary>
        /// Conteneur sur laquelle sera basé le service de navigation
        /// </summary>
        private Window MainWindow => Application.Current.MainWindow;

        /// <summary>
        /// Clef de la page courante
        /// </summary>
        public string CurrentPageKey { get; private set; }

        /// <summary>
        /// Retourne à la page précédente (Si aucune page précédente ne fait rien)
        /// </summary>
        public void GoBack()
        {
            if (_history.Any())
            {
                var page = _history.Pop();
                MainWindow.Content = page;

                var navigable = page.DataContext as INavigable;
                navigable?.OnNavigatedTo();
            }
        }

        /// <summary>
        /// Navigue vers la page correspondant à la clef
        /// </summary>
        /// <param name="pageKey">clef de la page</param>
        public void NavigateTo(string pageKey)
        {
            NavigateTo(pageKey, null);
        }

        public void NavigateTo(string pageKey, object parameter)
        {
            // historisation de l'ancienne page
            if (MainWindow.Content is Page oldPage)
            {
                _history.Push(oldPage);
            }

            // Affection de la vue
            var instance = (Page)Activator.CreateInstance(_pages[pageKey.ToUpper()]);
            MainWindow.Content = instance;

            var navigable = instance?.DataContext as INavigable;
            navigable?.OnNavigatedTo(parameter);
            CurrentPageKey = pageKey;
        }

        /// <summary>
        /// Configuration de la navigation
        /// </summary>
        /// <typeparam name="TPage">La page a associer</typeparam>
        /// <param name="key">La clef associée</param>
        public void Configure<TPage>(string key) where TPage : Page
        {
            _pages.Add(key.ToUpper(), typeof(TPage));
        }

        public void Configure(Type type, string key)
        {
            if (!typeof(Page).IsAssignableFrom(type))
            {
                throw new ArgumentException("Seule une page peut être enregistré pour le service de navigation");
            }
            _pages.Add(key.ToUpper(), type);
        }
    }

    /// <summary>
    /// WPF implementation of <see cref="INavigationService"/>.
    /// </summary>
    public class FrameNavigationService : INavigationService
    {
        /// <summary>
        /// page key <=> page type
        /// </summary>
        private readonly Dictionary<string, Type> configuration = new Dictionary<string, Type>();
        private Frame Frame => Application.Current.MainWindow.Content as Frame;

        /// <summary>
        /// The key corresponding to the currently displayed page.
        /// </summary>
        public string CurrentPageKey { get; private set; }

        /// <summary>
        /// If possible, discards the current page and displays the previous page
        /// on the navigation stack.
        /// </summary>
        public void GoBack()
        {
            if (Frame.CanGoBack)
            {
                Frame.GoBack();
            }
        }

        /// <summary>
        /// Displays a new page corresponding to the given key. 
        /// Make sure to call the <see cref="Configure"/>
        /// method first.
        /// </summary>
        /// <param name="pageKey">The key corresponding to the page
        /// that should be displayed.</param>
        /// <exception cref="ArgumentException">When this method is called for 
        /// a key that has not been configured earlier.</exception>
        public void NavigateTo(string pageKey) => NavigateTo(pageKey, null);

        /// <summary>
        /// Displays a new page corresponding to the given key,
        /// and passes a parameter to the new page.
        /// Make sure to call the <see cref="Configure"/>
        /// method first.
        /// </summary>
        /// <param name="pageKey">The key corresponding to the page
        /// that should be displayed.</param>
        /// <param name="parameter">The parameter that should be passed
        /// to the new page.</param>
        /// <exception cref="ArgumentException">When this method is called for 
        /// a key that has not been configured earlier.</exception>
        public void NavigateTo(string pageKey, object parameter)
        {
            lock (configuration)
            {
                if (!configuration.ContainsKey(pageKey))
                {
                    throw new ArgumentException($"No such page: {pageKey}. Did you forget to call NavigationService.Configure?", nameof(pageKey));
                }

                var page = Activator.CreateInstance(configuration[pageKey]);
                if (parameter != null)
                {
                    Frame.Navigate(page, parameter);
                }
                else
                {
                    Frame.Navigate(page);
                }
            }
        }

        /// <summary>
        /// Adds a key/page pair to the navigation service.
        /// </summary>
        /// <typeparam name="TView">The type of the page corresponding to the key.</typeparam>
        /// <param name="pageKey">The key that will be used for navigate to the related page.</param>
        public void Configure<TView>(string pageKey) where TView : Page
            => Configure(pageKey, typeof(TView));

        /// <summary>
        /// Adds a key/page pair to the navigation service.
        /// </summary>
        /// <param name="pageKey">The key that will be used for navigate to the related page.</typeparam>
        /// <param name="pageType">The type of the page corresponding to the key.</param>
        public void Configure(string pageKey, Type pageType)
        {
            lock (configuration)
            {
                if (configuration.ContainsKey(pageKey))
                {
                    throw new ArgumentException($"This key is already configured: {pageKey}");
                }

                if (configuration.Any(c => c.Value == pageType))
                {
                    throw new ArgumentException($"This type is already configured for the key: {configuration.First(c => c.Value == pageType).Key}");
                }

                if (!typeof(Page).IsAssignableFrom(pageType))
                {
                    throw new ArgumentException($"type parameter must inherit from System.Windows.Controls.Page: {pageType}", nameof(pageType));
                }

                configuration.Add(pageKey, pageType);
            }
        }
    }
}
