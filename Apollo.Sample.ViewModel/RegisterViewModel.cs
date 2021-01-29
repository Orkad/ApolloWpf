using Apollo.Sample.Model.Business;
using Apollo.Sample.Model.Business.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using System.Security;
using System.Windows.Input;

namespace Apollo.Sample.ViewModel
{
    public class RegisterViewModel : ViewModelBase
    {
        public RegisterViewModel(INavigationService navigationService, IDialogService dialogService, IAuthenticationService authenticationService)
        {
            this.navigationService = navigationService;
            this.dialogService = dialogService;
            this.authenticationService = authenticationService;
        }

        private string username;
        private SecureString password;
        private string email;
        private readonly INavigationService navigationService;
        private readonly IDialogService dialogService;
        private readonly IAuthenticationService authenticationService;

        public string Email { get => email; set => Set(ref email, value); }
        public string Username { get => username; set => Set(ref username, value); }
        public SecureString Password { get => password; set => Set(ref password, value); }

        public ICommand RegisterCommand => new RelayCommand(async () =>
        {
            try
            {
                authenticationService.Register(Email, Username, Password);
                navigationService.GoBack();
                await dialogService.ShowMessage("Votre inscription a bien été prise en compte, vous pouvez a présent vous authentifier", "Inscription réussie");
            }
            catch (BusinessException ex)
            {
                await dialogService.ShowError(ex, "Erreur lors de l'inscription", "Ok", null);
            }
        }, () => !string.IsNullOrEmpty(Email) && BusinessRules.Validation.ValidateUsername(Username) && BusinessRules.Validation.ValidatePassword(Password));
    }
}