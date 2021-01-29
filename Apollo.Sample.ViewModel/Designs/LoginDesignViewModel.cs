using System;
using System.Collections.Generic;
using System.Security;
using System.Text;
using System.Windows.Input;

namespace Apollo.Sample.ViewModel.Designs
{
    public class LoginDesignViewModel : ILoginViewModel
    {
        public ICommand LoginCommand => null;
        public SecureString Password { get; set; }
        public string Username { get; set; }
    }
}
