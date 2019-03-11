using mobiBooking.UWP.ViewModels;
using mobiBooking.UWP.Views;
using mobiBooking.UWP.Views.CustomDialogs;
using Newtonsoft.Json;
using RestSharp;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;
using System;

namespace mobiBooking.UWP
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();

            NavView.SelectedItem = NavView.MenuItems[0];
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            //LoginViewModel parameters = (LoginViewModel)e.Parameter;
        }

        private async void LogOut_Click(object sejnder, RoutedEventArgs e)
        {
            LoginViewModel objResponse = new LoginViewModel();
            string json = JsonConvert.SerializeObject(objResponse.Token);

            RestClient client = new RestClient("http://192.168.10.240:51290/api");
            // client.Authenticator = new HttpBasicAuthenticator(username, password);
            client.RemoteCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;

            RestRequest request = new RestRequest("Authenticate/Logout", Method.POST);
            request.AddParameter("Authorization", "Bearer " + objResponse.Token, ParameterType.HttpHeader);

            // execute the request
            IRestResponse response = client.Execute(request);
            string content = response.Content; // raw content as string

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                Frame.Navigate(typeof(LoginPage), null, new DrillInNavigationTransitionInfo());
            }
            else
            {
                ContentDialogResult error = await new ErrorDialog().ShowAsync();
            }
        }

        private void NavView_SelectionChanged(Microsoft.UI.Xaml.Controls.NavigationView sender, Microsoft.UI.Xaml.Controls.NavigationViewSelectionChangedEventArgs args)
        {
            if (args.SelectedItemContainer != null)
            {
                switch (args.SelectedItemContainer.Tag)
                {
                    case "dashboard":
                        {
                            PageTitle.Text = "Dashboard";
                            ContentFrame.Navigate(typeof(DashboardPage), null, new DrillInNavigationTransitionInfo());
                        }
                        break;

                    case "bookroom":
                        {
                            PageTitle.Text = "Zarezerwuj salę";
                            ContentFrame.Navigate(typeof(BookPage), null, new DrillInNavigationTransitionInfo());
                        }
                        break;
                    case "list":
                        {
                            PageTitle.Text = "Lista sal/rezerwacje";
                            ContentFrame.Navigate(typeof(ListPage), null, new DrillInNavigationTransitionInfo());
                        }
                        break;
                    case "addroom":
                        {
                            PageTitle.Text = "Dodaj salę";
                            ContentFrame.Navigate(typeof(AddRoomPage), null, new DrillInNavigationTransitionInfo());
                        }
                        break;

                    case "users":
                        {
                            PageTitle.Text = "Użytkownicy";
                            ContentFrame.Navigate(typeof(UsersPage), null, new DrillInNavigationTransitionInfo());
                        }
                        break;
                }
                if (args.IsSettingsSelected)
                {
                    PageTitle.Text = "Ustawienia";
                    ContentFrame.Navigate(typeof(SettingsPage), null, new DrillInNavigationTransitionInfo());
                }
            }
        }

        private void AddUser_Click(object sender, RoutedEventArgs e)
        {
            PageTitle.Text = "Dodaj użytkownika";
            ContentFrame.Navigate(typeof(AddUsersPage), null, new DrillInNavigationTransitionInfo());
        }
    }
}