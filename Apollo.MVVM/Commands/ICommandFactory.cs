using System;
using System.Windows.Input;

namespace Apollo.MVVM.Commands
{
    /// <summary>
    /// Factory a utiliser dans les view model afin d'utiliser les implémentations de ICommands relatives à la plateforme cible
    /// </summary>
    public interface ICommandFactory
    {
        ICommand CreateCommand<T>(Action<T> execute, Func<T, bool> canExecute);
        ICommand CreateCommand(Action execute, Func<bool> canExecute);
    }
}
