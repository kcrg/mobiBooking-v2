using mobiBooking.Core;
using MvvmCross.Platforms.Uap.Views;

namespace mobiBooking.UWP
{
    public abstract class UWPApplication : MvxApplication<Setup, CoreApp> { }

    public sealed partial class App : UWPApplication
    {
        public App()
        {
            InitializeComponent();
        }
    }
}