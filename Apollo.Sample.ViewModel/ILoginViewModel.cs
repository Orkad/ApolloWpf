using System.Security;
using System.Windows.Input;

namespace Apollo.Sample.ViewModel
{
    public interface ILoginViewModel
    {
        ICommand LoginCommand { get; }
        SecureString Password { get; set; }
        string Username { get; set; }
    }
}