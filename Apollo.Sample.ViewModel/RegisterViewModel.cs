using Apollo.MVVM.Navigation;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using System.Windows.Input;

namespace ApolloWpf.ViewModel
{
    public class RegisterViewModel : NavigableViewModelBase
    {
        public RegisterViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
            
        }
    }
}