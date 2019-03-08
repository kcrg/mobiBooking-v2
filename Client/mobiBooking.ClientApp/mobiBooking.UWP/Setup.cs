using mobiBooking.Core;
using MvvmCross.Platforms.Uap.Core;

namespace mobiBooking.UWP
{
    public sealed class Setup : MvxWindowsSetup<CoreApp>
    {
        protected override void InitializeFirstChance()
        {
            base.InitializeFirstChance();
        }

        protected override void InitializeLastChance()
        {
            base.InitializeLastChance();
        }
    }
}