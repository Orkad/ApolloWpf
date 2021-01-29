using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Apollo.Sample.ViewModel.Designs
{
    public class MainDesignViewModel : IMainViewModel
    {
        public string Profile { get; set; }

        public ICommand ProfileCommand => null;

        public ICommand ShowVersionCommand => null;
    }
}
