using System;
using System.Windows.Input;

using Xamarin.Forms;

namespace ControlCSA.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel()
        {
            Title = "Acerca De";

            OpenWebCommand = new Command(() => Device.OpenUri(new Uri("http://www.sanatorioaleman.cl/")));
        }

        public ICommand OpenWebCommand { get; }
    }
}