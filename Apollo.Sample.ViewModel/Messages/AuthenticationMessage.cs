using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Apollo.Sample.ViewModel.Messages
{
    public class AuthenticationMessage : MessageBase
    {
        public bool LoggedIn { get; set; }

        public string ProfileName { get; set; }
    }
}
