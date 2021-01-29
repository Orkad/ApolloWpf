using System.Security;
using System.Windows.Input;

namespace Apollo.Sample.ViewModel
{
    public interface IRegisterViewModel
    {
        string Email { get; set; }
        SecureString Password { get; set; }
        ICommand RegisterCommand { get; }
        string Username { get; set; }
    }
}