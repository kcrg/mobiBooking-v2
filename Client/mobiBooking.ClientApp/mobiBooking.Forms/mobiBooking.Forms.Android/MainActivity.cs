using Android.App;
using Android.Content.PM;
using Android.OS;
using mobiBooking.Core;
using MvvmCross.Forms.Platforms.Android.Views;

namespace mobiBooking.Forms.Droid
{
    [Activity(Label = "Mobi Booking", Icon = "@mipmap/icon", MainLauncher = true, Theme = "@style/MainTheme", NoHistory = false, ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : MvxFormsAppCompatActivity<Setup, CoreApp, App>
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            base.OnCreate(bundle);
        }
    }
}