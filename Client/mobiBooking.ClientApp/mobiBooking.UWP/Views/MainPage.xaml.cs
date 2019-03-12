using Microsoft.Toolkit.Uwp.Helpers;
using mobiBooking.UWP.ViewModels;
using mobiBooking.UWP.Views;
using mobiBooking.UWP.Views.CustomDialogs;
using Newtonsoft.Json;
using RestSharp;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;

namespace mobiBooking.UWP
{
    public sealed partial class MainPage : Page
    {
        private readonly LocalObjectStorageHelper helper = new LocalObjectStorageHelper();

        public MainPage()
        {
            InitializeComponent();

            NavView.SelectedItem = NavView.MenuItems[0];
            CheckUserType();
        }

        private async void CheckUserType()
        {
            LoginViewModel result = await helper.ReadFileAsync<LoginViewModel>("response");

            UserText.Content = result.Name + " - " + result.UserType;

            if (result.UserType == "User")
            {
                adduser.Visibility = Visibility.Collapsed;
                addroom.Visibility = Visibility.Collapsed;
            }
        }

        private async void LogOut_Click(object sejnder, RoutedEventArgs e)
        {
            LoginViewModel result = await helper.ReadFileAsync<LoginViewModel>("response");

            string json = JsonConvert.SerializeObject(result.Token);

            ConnectionModel IP = new ConnectionModel();
            RestClient client = new RestClient(IP.Adress);
            client.RemoteCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;

            RestRequest request = new RestRequest("Authenticate/Logout", Method.POST);
            request.AddParameter("Authorization", "Bearer " + result.Token, ParameterType.HttpHeader);

            // execute the request
            IRestResponse response = client.Execute(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                LoginViewModel responseObj = new LoginViewModel
                {
                    UserName = null,
                    Name = null,
                    Surname = null,
                    Email = null,
                    UserType = null,
                    Token = null
                };
                await helper.SaveFileAsync("response", responseObj);

                Frame.Navigate(typeof(LoginPage), null, new DrillInNavigationTransitionInfo());
            }
            else
            {
                await new CustomDialog("Wystąpił błąd podczas komunikacji z serwerem.", CustomDialog.Type.Error).ShowAsync();

                LoginViewModel responseObj = new LoginViewModel
                {
                    UserName = null,
                    Name = null,
                    Surname = null,
                    Email = null,
                    UserType = null,
                    Token = null
                };
                await helper.SaveFileAsync("response", responseObj);

                Frame.Navigate(typeof(LoginPage), null, new DrillInNavigationTransitionInfo());
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