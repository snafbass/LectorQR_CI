using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ControlCSA.Services;
using ControlCSA.Views;
using Xamarin.Essentials;

namespace ControlCSA
{
    public partial class App : Application
    {
        
        public App()
        {
            InitializeComponent();
           
            DependencyService.Register<MockDataStore>();
            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
            DeviceDisplay.KeepScreenOn = !DeviceDisplay.KeepScreenOn;
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            DeviceDisplay.KeepScreenOn = DeviceDisplay.KeepScreenOn;
            // Handle when your app resumes
        }
    }
}
