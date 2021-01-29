using System.Windows.Input;

namespace Apollo.Sample.ViewModel
{
    public interface IMainViewModel
    {
        string Profile { get; set; }
        ICommand ProfileCommand { get; }
        ICommand ShowVersionCommand { get; }
    }
}