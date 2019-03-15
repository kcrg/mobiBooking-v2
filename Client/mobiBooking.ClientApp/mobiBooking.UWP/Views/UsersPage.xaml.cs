using Microsoft.Toolkit.Uwp.Helpers;
using mobiBooking.UWP.Models;
using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace mobiBooking.UWP.Views
{
    public sealed partial class UsersPage : Page
    {
        private readonly LocalObjectStorageHelper helper = new LocalObjectStorageHelper();
        public UsersPage()
        {
            InitializeComponent();
        }

        private async Task GetUsers()
        {
            LoginModel SavedResponseObj = await helper.ReadFileAsync<LoginModel>("response");

            ConnectionModel IP = new ConnectionModel();
            RestClient client = new RestClient(IP.Adress);
            RestRequest request = new RestRequest("Users/get_all", Method.GET);
            request.AddParameter("Authorization", "Bearer " + SavedResponseObj.Token, ParameterType.HttpHeader);

            // execute the request
            IRestResponse response = client.Execute(request);

            List<GetUsersModel> usersList = new List<GetUsersModel>();
            usersList = JsonConvert.DeserializeAnonymousType(response.Content, usersList);

            UsersList.ItemsSource = usersList;
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await GetUsers();
        }
    }
}