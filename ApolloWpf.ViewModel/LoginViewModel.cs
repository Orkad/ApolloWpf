using ApolloWpf.Business;
using ApolloWpfCore.Abstract;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using System;
using System.Security;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ApolloWpf.ViewModel
{
    public class LoginViewModel:NavigableViewModelBase
    {
        private readonly IAuthenticationService _authenticationService;
        private string _username;
        private SecureString _password;

        public string Username
        {
            get => _username;
            set => Set(ref _username, value);
        }

        public SecureString Password
        {
            get => _password;
            set => Set(ref _password, value);
        }

        public ICommand LoginCommand { get; }


        public LoginViewModel(INavigationService navigationService, IDialogService dialogService, IAuthenticationService authenticationService)
            :base(navigationService, dialogService)
        {
            _authenticationService = authenticationService;
            LoginCommand = new RelayCommand(async () =>
            {
                try
                {
                    await Login();
                }
                catch (Exception exception)
                {
                    await ShowErrorAsync(exception.Message);
                }
            });
        }

        private async Task Login()
        {
            _authenticationService.Login(Username, Password);
            if (_authenticationService.IsLoggedIn())
            {
                await NavigateToAsync("users");
                return;
            }
            await ShowErrorAsync("utilisateur ou mot de passe incorrect");
        }
    }
}
