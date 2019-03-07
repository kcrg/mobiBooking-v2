using mobiBooking.Core.ViewModels;
using MvvmCross.IoC;
using MvvmCross.ViewModels;

namespace mobiBooking.Core
{
    public class CoreApp : MvxApplication
    {
        public override void Initialize()
        {
            base.Initialize();

            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();

            RegisterAppStart<MainPageViewModel>();
        }
    }
}