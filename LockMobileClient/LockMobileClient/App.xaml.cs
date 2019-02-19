using LockMobileClient.Services;
using LockMobileClient.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace LockMobileClient
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            if (SettingsService.DeviceId == "")
            {
                MainPage = new NavigationPage(new RegistrationPage());
            }
            else
            {

            }
        }

        public void DisplayErrorAlert(string msg)
        {
            MainPage.DisplayAlert("Device configuration error", msg, "OK");
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
