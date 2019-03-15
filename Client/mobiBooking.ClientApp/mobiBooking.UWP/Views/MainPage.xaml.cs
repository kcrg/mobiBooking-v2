using Microsoft.Toolkit.Uwp.Helpers;
using mobiBooking.UWP.Models;
using mobiBooking.UWP.Views;
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
            LoginModel SavedResponseObj = await helper.ReadFileAsync<LoginModel>("response");

            UserText.Content = SavedResponseObj.Name + " - " + SavedResponseObj.UserType;

            if (SavedResponseObj.UserType == "User")
            {
                addroom.Visibility = Visibility.Collapsed;

                usersSeparator.Visibility = Visibility.Collapsed;
                usersHeader.Visibility = Visibility.Collapsed;
                users.Visibility = Visibility.Collapsed;
                adduser.Visibility = Visibility.Collapsed;
            }
        }

        private async void LogOut_Click(object sejnder, RoutedEventArgs e)
        {
            LoginModel EmptyLoginResponseObj = new LoginModel
            {
                UserName = null,
                Name = null,
                Surname = null,
                Email = null,
                UserType = null,
                Token = null,
                IsLoged = false
            };
            await helper.SaveFileAsync("response", EmptyLoginResponseObj);

            Frame.Navigate(typeof(LoginPage), null, new DrillInNavigationTransitionInfo());
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
                            ContentFrame.Navigate(typeof(RoomListPage), null, new DrillInNavigationTransitionInfo());
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
                    case "addusers":
                        {
                            PageTitle.Text = "Dodaj użytkownika";
                            ContentFrame.Navigate(typeof(AddUsersPage), null, new DrillInNavigationTransitionInfo());
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
    }
}