using Microsoft.Toolkit.Uwp.Helpers;
using mobiBooking.UWP.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private IList<GetUsersModel> usersList = new ObservableCollection<GetUsersModel>();
        public UsersPage()
        {
            InitializeComponent();
        }

        private async Task GetUsers()
        {
            LoginModel SavedResponseObj = await helper.ReadFileAsync<LoginModel>("response");

            RestClient client = new RestClient(IP.Adress);
            RestRequest request = new RestRequest("Users/get_all", Method.GET);
            request.AddParameter("Authorization", "Bearer " + SavedResponseObj.Token, ParameterType.HttpHeader);

            // execute the request
            IRestResponse response = client.Execute(request);

            usersList = JsonConvert.DeserializeAnonymousType(response.Content, usersList);

            UsersList.ItemsSource = usersList;



            usersCount.Text = UsersList.Items.Count.ToString() + " użytkowników";
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await GetUsers();
        }

        private async void Delete_Click(object sender, RoutedEventArgs e)
        {
            await DeleteUser(sender);
            await GetUsers();
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            ListViewItem lvi = FindParent<ListViewItem>(button);
            lvi.IsSelected = true;

            GetUsersModel userObj = new GetUsersModel
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
        }

        private void AddUser_Click(object sender, RoutedEventArgs e)
        {
            EditOrAddUser(null);
        }

        private async void Activate_Click(object sender, RoutedEventArgs e) // woźniak #ruszdupe
        {
            LoginModel SavedResponseObj = await helper.ReadFileAsync<LoginModel>("response");

            Button button = sender as Button;
            ListViewItem lvi = FindParent<ListViewItem>(button);

            lvi.IsSelected = true;
            int idItem = usersList[UsersList.SelectedIndex].Id;

            AddUserModel userObj = new AddUserModel
            {
                Active = true
            };
            string json = JsonConvert.SerializeObject(userObj);

            RestClient client = new RestClient(IP.Adress);
            RestRequest request = new RestRequest("Account/update/{id}", Method.PUT);
            request.AddParameter("application/json", json, ParameterType.RequestBody);
            request.AddParameter("Authorization", "Bearer " + SavedResponseObj.Token, ParameterType.HttpHeader);
            request.AddParameter("id", idItem, ParameterType.UrlSegment);

            // execute the request
            IRestResponse response = client.Execute(request);
            string res = response.Content;
        }

        private async Task DeleteUser(object sender)
        {
            LoginModel SavedResponseObj = await helper.ReadFileAsync<LoginModel>("response");

            Button button = sender as Button;
            ListViewItem lvi = FindParent<ListViewItem>(button);

            lvi.IsSelected = true;
            int idItem = usersList[UsersList.SelectedIndex].Id;

            RestClient client = new RestClient(IP.Adress);
            RestRequest request = new RestRequest("Account/delete/{id}", Method.DELETE);
            request.AddParameter("Authorization", "Bearer " + SavedResponseObj.Token, ParameterType.HttpHeader);
            request.AddParameter("id", idItem, ParameterType.UrlSegment);
            IRestResponse response = client.Execute(request);
        }

        private void EditOrAddUser(object param) //TODO add parameter with user information
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