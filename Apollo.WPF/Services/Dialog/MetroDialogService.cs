using GalaSoft.MvvmLight.Views;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace Apollo.WPF.Services.Dialog
{
    public class MetroDialogService : IDialogService
    {
        private MetroWindow MainMetroWindow => (MetroWindow)Application.Current.MainWindow;

        private static MetroDialogSettings CustomSettings(Action<MetroDialogSettings> configuration)
        {
            var settings = new MetroDialogSettings
            {
                AnimateHide = false,
                AnimateShow = false,
            };
            configuration?.Invoke(settings);
            return settings;
        }

        public void Alert(string message, string title)
        {
            MainMetroWindow?.ShowModalMessageExternal(title, message);
        }

        public string Prompt(string message, string title)
        {
            return MainMetroWindow?.ShowModalInputExternal(title, message);
        }

        public bool Confirm(string message, string title, string trueLabel = "Ok", string falseLabel = "Annuler")
        {
            var metroDialogSettings = CustomSettings((c) =>
            {
                c.AffirmativeButtonText = trueLabel;
                c.NegativeButtonText = falseLabel;
            });
            var result = MainMetroWindow?.ShowModalMessageExternal(title, message, MessageDialogStyle.AffirmativeAndNegative, metroDialogSettings);
            return result == MessageDialogResult.Affirmative;
        }

        public async Task ShowError(string message, string title, string buttonText, Action afterHideCallback)
        {
            await MainMetroWindow?.ShowMessageAsync(title, message, MessageDialogStyle.Affirmative, CustomSettings((c) =>
            {
                c.AffirmativeButtonText = buttonText;
            }));
            afterHideCallback?.Invoke();
        }

        public async Task ShowError(Exception error, string title, string buttonText, Action afterHideCallback)
        {
            await ShowError(error.Message, title, buttonText, afterHideCallback);
        }

        public async Task ShowMessage(string message, string title)
        {
            await ShowMessage(message, title, "Ok", null);
        }

        public async Task ShowMessage(string message, string title, string buttonText, Action afterHideCallback)
        {
            await MainMetroWindow?.ShowMessageAsync(title, message, MessageDialogStyle.Affirmative, CustomSettings((c) =>
            {
                c.AffirmativeButtonText = buttonText;
            }));
            afterHideCallback?.Invoke();
        }

        public async Task<bool> ShowMessage(string message, string title, string buttonConfirmText, string buttonCancelText, Action<bool> afterHideCallback)
        {
            var result = await MainMetroWindow?.ShowMessageAsync(title, message, MessageDialogStyle.AffirmativeAndNegative, CustomSettings((c) =>
            {
                c.AffirmativeButtonText = buttonConfirmText;
                c.NegativeButtonText = buttonCancelText;
            }));
            return result == MessageDialogResult.Affirmative;
        }

        public async Task ShowMessageBox(string message, string title)
        {
            MainMetroWindow.ShowModalMessageExternal(title, message);
        }
    }
}
