using Microsoft.Toolkit.Uwp.Helpers;
using mobiBooking.Core.Models;
using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;
using System.Threading.Tasks;
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

        private async Task GetRooms()
        {
            LoginModel SavedResponseObj = await helper.ReadFileAsync<LoginModel>("response");

            ConnectionModel IP = new ConnectionModel();
            RestClient client = new RestClient(IP.Adress);
            RestRequest request = new RestRequest("Room/get_all", Method.GET);
            _ = request.AddParameter("Authorization", "Bearer " + SavedResponseObj.Token, ParameterType.HttpHeader);
            // execute the request
            IRestResponse response = client.Execute(request);

            List<GetRoomsModel> roomsList = new List<GetRoomsModel>();
            roomsList = JsonConvert.DeserializeAnonymousType(response.Content, roomsList);

            UsersList.ItemsSource = roomsList;
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await GetRooms();
        }
    }
}