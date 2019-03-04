using LockMobileClient.Services;
using LockMobileClient.ViewModels;
using LockMobileClient.Views;
using Unity;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace LockMobileClient
{
    public partial class App : Application
    {
        public IUnityContainer Container { get; } = new UnityContainer();

        public App()
        {
            InitializeComponent();
            ContainerSetting();

            if (SettingsService.DeviceId == "")
            {
                MainPage = new NavigationPage(new RegistrationPage());
            }
            else
            {

            }
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

        void ContainerSetting()
        {
            Container.RegisterType<RegistrationViewModel>();
        }
    }
}
