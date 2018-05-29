using ApolloWpfCore.Abstract;
using GalaSoft.MvvmLight.Views;

namespace ApolloWpf.ViewModel
{
    public class MainViewModel : NavigableViewModelBase
    {
        private UsersViewModel _usersViewModel;

        public MainViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {

        }

        public UsersViewModel UsersViewModel
        {
            get => _usersViewModel;
            set => Set(ref _usersViewModel, value);
        }


    }
}
