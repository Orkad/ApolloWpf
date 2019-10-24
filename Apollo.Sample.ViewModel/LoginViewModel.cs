using Apollo.MVVM.Navigation;
using Apollo.Sample.Model.Business.Services;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using System;
using System.Security;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Apollo.Sample.ViewModel
{
    public class LoginViewModel : NavigableViewModelBase
    {
        private readonly IAuthenticationService _authenticationService;
        private string username;
        private SecureString password;

        public string Username
        {
            get => username;
            set => Set(ref username, value);
        }

        public SecureString Password
        {
            get => password;
            set => Set(ref password, value);
        }

        public ICommand LoginCommand { get; }

        public LoginViewModel(INavigationService navigationService, IDialogService dialogService, IAuthenticationService authenticationService)
            : base(navigationService, dialogService)
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
            _authenticationService.Login(username, password);
            if (_authenticationService.IsLoggedIn())
            {
                await NavigateToAsync("users");
                return;
            }
            await ShowErrorAsync("utilisateur ou mot de passe incorrect");
        }
    }
}