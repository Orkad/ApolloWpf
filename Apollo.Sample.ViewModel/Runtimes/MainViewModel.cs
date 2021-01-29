using Apollo.MVVM.Navigation;
using Apollo.Sample.ViewModel.Messages;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using System;
using System.Windows.Input;

namespace Apollo.Sample.ViewModel.Runtimes
{
    public class MainViewModel : NavigableViewModelBase, IMainViewModel
    {
        private string profile;

        public ICommand ShowVersionCommand { get; }

        /// <summary>
        /// Affiche le profil ou bien l'écran de login 
        /// </summary>
        public ICommand ProfileCommand { get; }

        public MainViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
            ShowVersionCommand = new RelayCommand(async () => await _dialogService.ShowMessage("Version 1.0.0.0", "Numéro de version"));
            ProfileCommand = new RelayCommand(() => navigationService.NavigateTo("LoginPage"));
            MessengerInstance.Register<AuthenticationMessage>(this, OnAuthenticationMessage);
            Profile = "non connecté";
        }

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

        public string Profile { get => profile; set => Set(ref profile, value); }
    }
}