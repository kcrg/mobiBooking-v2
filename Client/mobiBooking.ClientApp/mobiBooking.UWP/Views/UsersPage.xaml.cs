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
using Windows.UI.Xaml.Controls.Primitives;
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
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await GetUsers();
        } 

        private void Options_Click(object sender, RoutedEventArgs e)
        {
            FlyoutBase.ShowAttachedFlyout((FrameworkElement)sender);
        }

        private async void Delete_Click(object sender, RoutedEventArgs e)
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

            await GetUsers();
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            MainPage homePage = rootFrame.Content as MainPage;
            homePage.NavigationFrame.Navigate(typeof(AddUsersPage), null, new DrillInNavigationTransitionInfo());
        }

        private void AddUser_Click(object sender, RoutedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            MainPage homePage = rootFrame.Content as MainPage;
            homePage.NavigationFrame.Navigate(typeof(AddUsersPage), null, new DrillInNavigationTransitionInfo());
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