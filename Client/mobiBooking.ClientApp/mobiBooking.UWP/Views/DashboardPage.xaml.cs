using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Media.Imaging;

namespace mobiBooking.UWP.Views
{
    public sealed partial class DashboardPage : Page
    {
        private readonly BitmapImage LogoWhite = new BitmapImage(new Uri("ms-appx:///Assets/DashboardAssets/CoffeeIconWhite.png"));
        private readonly BitmapImage LogoBlack = new BitmapImage(new Uri("ms-appx:///Assets/DashboardAssets/CoffeeIconBlack.png"));
        public DashboardPage()
        {
            InitializeComponent();
        }

        private void ChangeThemeLogo()
        {
            if (Application.Current.RequestedTheme == ApplicationTheme.Dark)
            {
                MeetingsTodayImage.Source = LogoWhite;
            }

            else
            {
                MeetingsTodayImage.Source = LogoBlack;
            }
        }

        private void BookRoomButton_Click(object sender, RoutedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            MainPage homePage = rootFrame.Content as MainPage;
            _ = homePage.NavigationFrame.Navigate(typeof(BookPage), null, new DrillInNavigationTransitionInfo());
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ChangeThemeLogo();
        }

        private void Page_GotFocus(object sender, RoutedEventArgs e)
        {
            ChangeThemeLogo();
        }
    }
}