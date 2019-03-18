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
            //MainPage nav = new MainPage();

            Frame rootFrame = Window.Current.Content as Frame;
            MainPage homePage = rootFrame.Content as MainPage;
            //nav.NView.SelectedItem = nav.NView.MenuItems[1];
            homePage.NavigationFrame.Navigate(typeof(BookPage), null, new DrillInNavigationTransitionInfo());
            //LoadingScreen.IsLoading = true;
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