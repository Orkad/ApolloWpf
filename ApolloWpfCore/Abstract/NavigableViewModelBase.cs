using ApolloWpfCore.Interfaces;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ApolloWpfCore.Abstract
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

        public virtual void OnNavigatedTo(object parameter = null) {}

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

        public async Task ShowInfoAsync(string message)
        {
            await _dialogService.ShowMessage(message, "Information");
        }

        public async Task ShowErrorAsync(string message)
        {
            await _dialogService.ShowError(message, "Erreur", "Ok", null);
        }

        public async Task<bool> AskOkOrCancelAsync(string message, string title)
        {
            return await _dialogService.ShowMessage(message, title, "Ok", "Annuler", null);
        }

        public async Task<bool> AskYesOrNoAsync(string message, string title)
        {
            return await _dialogService.ShowMessage(message, title, "Oui", "Non", null);
        }
    }
}
