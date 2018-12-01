using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;

//[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace XamarinAndroidEntry
{
    public partial class App : Xamarin.Forms.Application
    {
        public App(ISoftwareKeyboardService softwarekeyboardservice)
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage(softwarekeyboardservice));
        }

        protected override void OnStart()
        {
            App.Current.On<Android>().UseWindowSoftInputModeAdjust(WindowSoftInputModeAdjust.Resize);
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
