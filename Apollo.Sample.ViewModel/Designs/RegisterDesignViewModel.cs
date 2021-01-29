using System.Security;
using System.Windows.Input;

namespace Apollo.Sample.ViewModel.Designs
{
    public class RegisterDesignViewModel : IRegisterViewModel
    {
        public string Email { get; set; }
        public SecureString Password { get; set; }

        public ICommand RegisterCommand => null;

        public string Username { get; set; }
    }
}
