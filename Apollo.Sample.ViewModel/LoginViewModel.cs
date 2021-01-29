using Apollo.MVVM.Commands;
using Apollo.Sample.Model.Business;
using Apollo.Sample.Model.Business.Services;
using Apollo.Sample.ViewModel.Messages;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using System.Security;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Apollo.Sample.ViewModel
{
    public class LoginViewModel : ViewModelBase
    {
        private readonly INavigationService navigationService;
        private readonly IDialogService dialogService;
        private readonly IAuthenticationService authenticationService;
        private readonly ICommandFactory commandFactory;

        public LoginViewModel(INavigationService navigationService,
            IDialogService dialogService,
            IAuthenticationService authenticationService,
            ICommandFactory commandFactory)
        {
            this.navigationService = navigationService;
            this.dialogService = dialogService;
            this.authenticationService = authenticationService;
            this.commandFactory = commandFactory;

        }

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

        public ICommand LoginCommand => commandFactory.CreateCommand(async () => await LoginAction(), CanLogin);
        public ICommand RegisterCommand => commandFactory.CreateCommand(() => navigationService.NavigateTo("RegisterPage"), () => true);

        private async Task LoginAction()
        {
            try
            {
                var user = authenticationService.Login(username, password);
                if (authenticationService.IsLoggedIn())
                {
                    navigationService.GoBack();
                    navigationService.NavigateTo("IndexPage");
                    MessengerInstance.Send(new AuthenticationMessage { LoggedIn = true, ProfileName = $"{user.FirstName} {user.LastName}" });
                    return;
                }
            }
            catch (BusinessException exception)
            {
                await dialogService.ShowError(exception, "Impossible de se connecter", "Ok", null);
            }
        }

        private bool CanLogin()
            => BusinessRules.Validation.ValidatePassword(password)
            && BusinessRules.Validation.ValidateUsername(username);
    }
}