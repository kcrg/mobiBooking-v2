﻿using mobiBooking.Core;
using MvvmCross.Forms.Platforms.Android.Core;

namespace mobiBooking.Forms.Droid
{
    public sealed class Setup : MvxFormsAndroidSetup<CoreApp, App>
    {
        protected override void InitializeLastChance()
        {
            //  Mvx.IoCProvider.RegisterSingleton(typeof(IOperatingSystemService), new OperatingSystemService());

            // Mvx.IoCProvider.ConstructAndRegisterSingleton<IMvxAppStart, MvxAppStart<MainPageViewModel>>();

            base.InitializeLastChance();
        }

        //public override IEnumerable<Assembly> GetViewModelAssemblies()
        //{
        //    List<Assembly> list = new List<Assembly>();
        //    list.AddRange(base.GetViewModelAssemblies());
        //    list.Add(typeof(NewsfeedItemViewModel).Assembly);
        //    return list.ToArray();
        //}
    }
}