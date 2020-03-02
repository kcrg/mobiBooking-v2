using Microsoft.Toolkit.Uwp.Helpers;
using mobiBooking.Core.Models;
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
    public sealed partial class RoomListPage : Page
    {
        private readonly LocalObjectStorageHelper helper = new LocalObjectStorageHelper();
        private readonly ConnectionModel IP = new ConnectionModel();
        private List<GetRoomsModel> roomList = new List<GetRoomsModel>();
        public RoomListPage()
        {
            InitializeComponent();
        }

        private async Task GetRooms()
        {
            LoginModel SavedResponseObj = await helper.ReadFileAsync<LoginModel>("response");

            RestClient client = new RestClient(IP.Adress);
            RestRequest request = new RestRequest("Room/get_all", Method.GET);
            _ = request.AddParameter("Authorization", "Bearer " + SavedResponseObj.Token, ParameterType.HttpHeader);
            IRestResponse response = client.Execute(request);

            roomList = JsonConvert.DeserializeAnonymousType(response.Content, roomList);
            RoomList.ItemsSource = roomList;
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await GetRooms();
        }

        private async void Delete_Click(object sender, RoutedEventArgs e)
        {
            IsEnabled = false;
            await DeleteRoom(sender);
            await GetRooms();
            IsEnabled = true;
        }

        private void Book_Click(object sender, RoutedEventArgs e)
        {
            IsEnabled = false;
            AppBarButton button = sender as AppBarButton;
            ListViewItem lvi = FindParent<ListViewItem>(button);
            lvi.IsSelected = true;

            GetRoomsModel roomObj = new GetRoomsModel
            {
                RoomName = roomList[RoomList.SelectedIndex].RoomName,
                Location = roomList[RoomList.SelectedIndex].Location,
                Availability = roomList[RoomList.SelectedIndex].Availability,
                AvailabilityId = roomList[RoomList.SelectedIndex].AvailabilityId - 1,
                NumberOfPeople = roomList[RoomList.SelectedIndex].NumberOfPeople,
                Id = roomList[RoomList.SelectedIndex].Id
            };

            EditOrBookRoom(JsonConvert.SerializeObject(roomObj), typeof(BookPage));
            IsEnabled = true;
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            IsEnabled = false;
            AppBarButton button = sender as AppBarButton;
            ListViewItem lvi = FindParent<ListViewItem>(button);
            lvi.IsSelected = true;

            GetRoomsModel roomObj = new GetRoomsModel
            {
                RoomName = roomList[RoomList.SelectedIndex].RoomName,
                Location = roomList[RoomList.SelectedIndex].Location,
                Availability = roomList[RoomList.SelectedIndex].Availability,
                AvailabilityId = roomList[RoomList.SelectedIndex].AvailabilityId - 1,
                NumberOfPeople = roomList[RoomList.SelectedIndex].NumberOfPeople,
                Id = roomList[RoomList.SelectedIndex].Id
            };

            EditOrBookRoom(JsonConvert.SerializeObject(roomObj), typeof(AddRoomPage));
            IsEnabled = true;
        }

        private void EditOrBookRoom(object param, Type page)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            MainPage homePage = rootFrame.Content as MainPage;
            homePage.NavigationFrame.Navigate(page, param, new DrillInNavigationTransitionInfo());
        }

        private async Task DeleteRoom(object sender)
        {
            LoginModel SavedResponseObj = await helper.ReadFileAsync<LoginModel>("response");

            AppBarButton button = sender as AppBarButton;
            ListViewItem lvi = FindParent<ListViewItem>(button);
            lvi.IsSelected = true;

            RestClient client = new RestClient(IP.Adress);
            RestRequest request = new RestRequest("Room/delete/{id}", Method.DELETE);
            _ = request.AddParameter("Authorization", "Bearer " + SavedResponseObj.Token, ParameterType.HttpHeader);
            _ = request.AddParameter("id", roomList[RoomList.SelectedIndex].Id, ParameterType.UrlSegment);
            _ = client.Execute(request);
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