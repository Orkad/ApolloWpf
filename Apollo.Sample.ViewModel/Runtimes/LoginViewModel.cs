using Apollo.MVVM;
using Apollo.MVVM.Navigation;
using Apollo.Sample.Model.Business;
using Apollo.Sample.Model.Business.Services;
using Apollo.Sample.ViewModel.Messages;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using System;
using System.Security;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Apollo.Sample.ViewModel.Runtimes
{
    public class LoginViewModel : NavigableViewModelBase, ILoginViewModel
    {
        private readonly IDialogService dialogService;
        private readonly IAuthenticationService authenticationService;
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

        public LoginViewModel(INavigationService navigationService, IDialogService dialogService, IAuthenticationService authenticationService)
            : base(navigationService, dialogService)
        {
            this.dialogService = dialogService;
            this.authenticationService = authenticationService;
        }

        public ICommand LoginCommand => new RelayCommand(async () =>
        {
            try
            {
                var user = authenticationService.Login(username, password);
                if (authenticationService.IsLoggedIn())
                {
                    await GoBackAsync();
                    MessengerInstance.Send(new AuthenticationMessage { LoggedIn = true, ProfileName = $"{user.FirstName} {user.LastName}" });
                    return;
                }
            }
            catch (BusinessException exception)
            {
                await dialogService.ShowError(exception, "Impossible de se connecter", "Ok", null);
            }
        },
            () => BusinessRules.Validation.ValidatePassword(password)
            && BusinessRules.Validation.ValidateUsername(username));

    }
}