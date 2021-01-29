using GalaSoft.MvvmLight.Messaging;

namespace Apollo.Sample.ViewModel.Messages
{
    public class AuthenticationMessage : MessageBase
    {
        public bool LoggedIn { get; set; }

        public string ProfileName { get; set; }
    }
}
