using ApolloWpfCore.Abstract;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using System.Windows.Input;

namespace ApolloWpf.ViewModel
{
    public class MainViewModel : NavigableViewModelBase
    {
        public ICommand ShowVersionCommand { get; }

        public MainViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
            ShowVersionCommand = new RelayCommand(async() => await _dialogService.ShowMessage("Version 1.0.0.0", "Numéro de version"));
        }
    }
}
