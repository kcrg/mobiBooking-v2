using Microsoft.Toolkit.Uwp.Helpers;
using mobiBooking.Core.Models;
using RestSharp;
using System;
using System.Threading.Tasks;
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

        private readonly LocalObjectStorageHelper helper = new LocalObjectStorageHelper();
        private readonly ConnectionModel IP = new ConnectionModel();
        public DashboardPage()
        {
            InitializeComponent();
        }
        private async Task<string> GetRequest(string resource, bool hours)
        {
            LoginModel SavedResponseObj = await helper.ReadFileAsync<LoginModel>("response");
            RestClient client = new RestClient(IP.Adress);
            IRestRequest request = new RestRequest(resource, Method.GET).AddParameter("Authorization", "Bearer " + SavedResponseObj.Token, ParameterType.HttpHeader);
            IRestResponse response = client.Execute(request);

            if (hours)
            {
                return response.Content.Replace("\x22", "") + "h";
            }
            else
            {
                return response.Content.Replace("\x22", "");
            }
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

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ChangeThemeLogo();

            ThisWeek.Text = await GetRequest("Meetings/this_week", true);
            LastWeek.Text = await GetRequest("Meetings/last_week", true);
            ThisMonth.Text = await GetRequest("Meetings/this_month", true);
            LastMonth.Text = await GetRequest("Meetings/last_month", true);

            ReservatedRooms.Text = await GetRequest("Room/get_reservated", false);
            NotReservatedRooms.Text = await GetRequest("Room/get_not_reservated", false);

            StatsThisWeek.Text = await GetRequest("Meetings/count_this_week", false);
            StatsLastWeek.Text = await GetRequest("Meetings/count_last_week", false);
            StatsThisMonth.Text = await GetRequest("Meetings/count_this_month", false);
            StatsLastMonth.Text = await GetRequest("Meetings/count_last_month", false);
        }

        private void Page_GotFocus(object sender, RoutedEventArgs e)
        {
            ChangeThemeLogo();
        }
    }
}