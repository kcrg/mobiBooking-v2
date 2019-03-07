using mobiBooking.Core.ViewModels;
using MvvmCross.Forms.Views;
using Xamarin.Forms.Xaml;

namespace mobiBooking.XamarinForms.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : MvxContentPage<MainPageViewModel>
    {
        public MainPage()
        {
            InitializeComponent();
        }
    }
}