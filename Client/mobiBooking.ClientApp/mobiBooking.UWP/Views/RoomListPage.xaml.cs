using Microsoft.Toolkit.Uwp.Helpers;
using mobiBooking.UWP.Models;
using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace mobiBooking.UWP.Views
{
    public sealed partial class RoomListPage : Page
    {
        private readonly LocalObjectStorageHelper helper = new LocalObjectStorageHelper();
        public RoomListPage()
        {
            InitializeComponent();
        }

        private async void GetRooms()
        {
            LoginModel SavedResponseObj = await helper.ReadFileAsync<LoginModel>("response");

            ConnectionModel IP = new ConnectionModel();
            RestClient client = new RestClient(IP.Adress);
            RestRequest request = new RestRequest("Room/get_all", Method.GET);
            request.AddParameter("Authorization", "Bearer " + SavedResponseObj.Token, ParameterType.HttpHeader);

            // execute the request
            IRestResponse response = client.Execute(request);

            List<GetUsersModel> usersList = new List<GetUsersModel>();
            usersList = JsonConvert.DeserializeAnonymousType(response.Content, usersList);

            UsersList.ItemsSource = usersList;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            GetRooms();
        }
    }
}