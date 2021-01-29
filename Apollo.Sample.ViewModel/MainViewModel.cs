using Apollo.MVVM.Commands;
using Apollo.Sample.ViewModel.Messages;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Apollo.Sample.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel(INavigationService navigationService, IDialogService dialogService, ICommandFactory commandFactory)
        {
            MessengerInstance.Register<AuthenticationMessage>(this, OnAuthenticationMessage);
            Profile = "non connecté";
            this.navigationService = navigationService;
            this.dialogService = dialogService;
            this.commandFactory = commandFactory;
        }

        private readonly INavigationService navigationService;
        private readonly IDialogService dialogService;
        private readonly ICommandFactory commandFactory;

        private string profile;

        public ICommand ShowVersionCommand => commandFactory.CreateCommand(async () => await ShowVersionAction(), () => true);
        public ICommand ProfileCommand => commandFactory.CreateCommand(() => navigationService.NavigateTo("LoginPage"), () => true);
        public string Profile { get => profile; set => Set(ref profile, value); }

        private void OnAuthenticationMessage(AuthenticationMessage msg)
        {
            if (!msg.LoggedIn)
            {
                Profile = "non connecté";
            }
            else
            {
                Profile = msg.ProfileName;
            }
        }

        private async Task ShowVersionAction()
        {
            await dialogService.ShowMessage("Version 1.0.0.0", "Numéro de version");
        }
    }
}