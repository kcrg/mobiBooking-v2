using mobiBooking.Core;
using mobiBooking.XamarinForms;
using MvvmCross.Forms.Platforms.Android.Core;

namespace mobiBooking.Xamarin.Droid
{
    public sealed class Setup : MvxFormsAndroidSetup<CoreApp, App>
    {
        protected override void InitializeLastChance()
        {
            base.InitializeLastChance();
        }
    }
}