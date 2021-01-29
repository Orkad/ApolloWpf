using Apollo.MVVM.Commands;
using System;
using System.Windows.Input;

namespace Apollo.WPF.Factories
{
    /// <summary>
    /// Création correcte des RelayCommand supporté en WPF
    /// </summary>
    public class WpfCommandFactory : ICommandFactory
    {
        public ICommand CreateCommand<T>(Action<T> execute, Func<T, bool> canExecute)
        {
            return new GalaSoft.MvvmLight.CommandWpf.RelayCommand<T>(execute, canExecute);
        }

        public ICommand CreateCommand(Action execute, Func<bool> canExecute)
        {
            return new GalaSoft.MvvmLight.CommandWpf.RelayCommand(execute, canExecute);
        }
    }
}
