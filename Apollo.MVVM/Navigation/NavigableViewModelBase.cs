using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Apollo.MVVM.Navigation
{
    public abstract class NavigableViewModelBase : ViewModelBase, INavigable
    {
        protected readonly INavigationService _navigationService;
        protected readonly IDialogService _dialogService;

        public ICommand BackCommand { get; }
        public ICommand NavigateCommand { get; }

        protected NavigableViewModelBase(INavigationService navigationService, IDialogService dialogService)
        {
            _navigationService = navigationService;
            _dialogService = dialogService;
            BackCommand = new RelayCommand(async () => await GoBackAsync());
            NavigateCommand = new RelayCommand<string>(async (key) => await NavigateToAsync(key, this));
        }

        public virtual void OnNavigatedTo(object parameter = null) { }

        public virtual void OnBack() { }

        /// <summary>
        /// Permet d'invalider toute navigation
        /// </summary>
        /// <returns></returns>
        protected virtual async Task<bool> ValidateNavigation()
        {
            return await Task.Run(() => true);
        }

        /// <summary>
        /// Retourne vers la page précédente
        /// </summary>
        protected async Task GoBackAsync()
        {
            if (!await ValidateNavigation())
            {
                return;
            }
            OnBack();
            _navigationService.GoBack();
        }

        /// <summary>
        /// Navigue vers la clef passé en paramètre
        /// </summary>
        /// <param name="key">clef correspondant a la page souhaitée</param>
        /// <param name="parameter">paramètre a transmettre au ViewModel de cette page</param>
        protected async Task NavigateToAsync(string key, object parameter = null)
        {
            if (!await ValidateNavigation())
            {
                return;
            }
            if (parameter == null)
            {
                _navigationService.NavigateTo(key);
            }
            else
            {
                _navigationService.NavigateTo(key, parameter);
            }
        }

        /// <summary>
        /// Affiche des informations à l'utilisateur via une boite de dialogue
        /// </summary>
        /// <param name="message">contenu de l'information</param>
        public async Task ShowInfoAsync(string message)
        {
            await _dialogService.ShowMessage(message, "Information");
        }

        /// <summary>
        /// Affiche un message d'érreur à l'utilisateur via une boite de dialogue
        /// </summary>
        /// <param name="message">contenu du message d'érreur</param>
        public async Task ShowErrorAsync(string message)
        {
            await _dialogService.ShowError(message, "Erreur", "Ok", null);
        }

        /// <summary>
        /// Demande une validation à l'utilisateur
        /// </summary>
        /// <param name="message">message / question a afficher à l'utilisateur</param>
        /// <param name="title">titre de la boite de dialogue</param>
        /// <param name="confirmText">Texte de confirmation</param>
        /// <param name="cancelText">Text d'annulation</param>
        public async Task<bool> ConfirmAsync(string message, string title = "Confirmation", string confirmText = "Ok", string cancelText = "Annuler")
        {
            return await _dialogService.ShowMessage(message, title, confirmText, cancelText, null);
        }
    }
}
