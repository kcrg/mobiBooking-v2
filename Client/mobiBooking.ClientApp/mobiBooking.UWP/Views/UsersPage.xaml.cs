using Microsoft.Toolkit.Uwp.Helpers;
using mobiBooking.Core.Models;
using mobiBooking.UWP.Views.CustomDialogs;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;

namespace mobiBooking.UWP.Views
{
    public sealed partial class UsersPage : Page
    {
        private readonly LocalObjectStorageHelper helper = new LocalObjectStorageHelper();
        private readonly ConnectionModel IP = new ConnectionModel();
        private List<Models.GetUsersModel> usersList = new List<Models.GetUsersModel>();
        public UsersPage()
        {
            InitializeComponent();
        }

        private async Task GetUsers()
        {
            LoginModel SavedResponseObj = await helper.ReadFileAsync<LoginModel>("response");

            RestClient client = new RestClient(IP.Adress);
            RestRequest request = new RestRequest("Users/get_all", Method.GET);
            _ = request.AddParameter("Authorization", "Bearer " + SavedResponseObj.Token, ParameterType.HttpHeader);
            IRestResponse response = client.Execute(request);

            usersList = JsonConvert.DeserializeAnonymousType(response.Content, usersList);
            usersList.ForEach(CheckActive);

            UsersList.ItemsSource = usersList;
            usersCount.Text = UsersList.Items.Count.ToString() + " użytkowników";
        }

        private void CheckActive(Models.GetUsersModel obj)
        {
            if (obj.Active)
            {
                obj.ActiveIcon = new SymbolIcon(Symbol.BlockContact);
                obj.ActiveString = "Aktywny";
            }
            else
            {
                obj.ActiveIcon = new SymbolIcon(Symbol.AddFriend);
                obj.ActiveString = "Nieaktywny";
            }
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await GetUsers();
        }

        private async void Delete_Click(object sender, RoutedEventArgs e)
        {
            IsEnabled = false;
            await DeleteUser(sender);
            await GetUsers();
            IsEnabled = true;
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            IsEnabled = false;
            Button button = sender as Button;
            ListViewItem lvi = FindParent<ListViewItem>(button);
            lvi.IsSelected = true;

            Models.GetUsersModel userObj = new Models.GetUsersModel
            {
                UserName = usersList[UsersList.SelectedIndex].UserName,
                Name = usersList[UsersList.SelectedIndex].Name,
                Surname = usersList[UsersList.SelectedIndex].Surname,
                Email = usersList[UsersList.SelectedIndex].Email,
                Active = usersList[UsersList.SelectedIndex].Active,
                Role = usersList[UsersList.SelectedIndex].Role,
                Id = usersList[UsersList.SelectedIndex].Id
            };

            EditOrAddUser(JsonConvert.SerializeObject(userObj));
            IsEnabled = true;
        }

        private void AddUser_Click(object sender, RoutedEventArgs e)
        {
            EditOrAddUser(null);
        }

        private async void Activate_Click(object sender, RoutedEventArgs e)
        {
            IsEnabled = false;
            await ActivateUser(sender);
            await GetUsers();
            IsEnabled = true;
        }

        private async Task DeleteUser(object sender)
        {
            LoginModel SavedResponseObj = await helper.ReadFileAsync<LoginModel>("response");

            Button button = sender as Button;
            ListViewItem lvi = FindParent<ListViewItem>(button);
            lvi.IsSelected = true;

            RestClient client = new RestClient(IP.Adress);
            RestRequest request = new RestRequest("Account/delete/{id}", Method.DELETE);
            _ = request.AddParameter("Authorization", "Bearer " + SavedResponseObj.Token, ParameterType.HttpHeader);
            _ = request.AddParameter("id", usersList[UsersList.SelectedIndex].Id, ParameterType.UrlSegment);
            _ = client.Execute(request);
        }

        private async Task ActivateUser(object sender)
        {
            LoginModel SavedResponseObj = await helper.ReadFileAsync<LoginModel>("response");

            AppBarButton button = sender as AppBarButton;
            ListViewItem lvi = FindParent<ListViewItem>(button);
            lvi.IsSelected = true;

            RestClient client = new RestClient(IP.Adress);
            RestRequest request = new RestRequest("Account/update_activity/{id}/{activity}", Method.PUT);
            _ = request.AddParameter("Authorization", "Bearer " + SavedResponseObj.Token, ParameterType.HttpHeader);
            _ = request.AddParameter("id", usersList[UsersList.SelectedIndex].Id, ParameterType.UrlSegment);
            _ = request.AddParameter("activity", !usersList[UsersList.SelectedIndex].Active, ParameterType.UrlSegment);
            IRestResponse response = client.Execute(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                button.Icon = new SymbolIcon(Symbol.AddFriend);
            }
            else
            {
                _ = await new CustomDialog("Wystąpił błąd podczas komunikacji z serwerem.", response.StatusCode.ToString(), CustomDialog.Type.Error).ShowAsync();
            }
        }

        private void EditOrAddUser(object param)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            MainPage homePage = rootFrame.Content as MainPage;
            homePage.NavigationFrame.Navigate(typeof(AddUsersPage), param, new DrillInNavigationTransitionInfo());
        }

        private static T FindParent<T>(DependencyObject dependencyObject) where T : DependencyObject
        {
            DependencyObject parent = VisualTreeHelper.GetParent(dependencyObject);
            if (parent == null)
            {
                return null;
            }

            T parentT = parent as T;
            return parentT ?? FindParent<T>(parent);
        }
    }
}