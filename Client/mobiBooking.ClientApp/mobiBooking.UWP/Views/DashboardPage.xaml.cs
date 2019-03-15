using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace mobiBooking.UWP.Views
{
    public sealed partial class DashboardPage : Page
    {
        public DashboardPage()
        {
            InitializeComponent();
        }

        private void BookRoomButton_Click(object sender, RoutedEventArgs e)
        {
            //LoadingScreen.IsLoading = true;
        }
    }
}