
using Android.App;
using Android.Content.PM;
using Android.OS;

namespace XamarinAndroidEntry.Droid
{
    [Activity(Label = "XamarinAndroidEntry", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            // if you need to size the screen yourself, register this service to get notified when 
            // the keyboard appears or disappears and get the current keyboard height along with it.
            // It also has a property if the keyboard is currently visible
            var softwarekeyboardservice = new SoftwareKeyboardService(this);

            // in your app you would probably Resolve this through some sort of IOC container
            LoadApplication(new App(softwarekeyboardservice));
        }
    }
}