using Apollo.MVVM.Navigation;
using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Apollo.WPF.Services.Navigation
{
    public class FrameNavigationService : INavigationService
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
}
