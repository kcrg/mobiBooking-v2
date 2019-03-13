using Microsoft.Toolkit.Uwp.Helpers;
using mobiBooking.UWP.Models;
using mobiBooking.UWP.Views.CustomDialogs;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace mobiBooking.UWP.Views
{
    public sealed partial class BookPage : Page
    {
        private readonly LocalObjectStorageHelper helper = new LocalObjectStorageHelper();
        public BookPage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            GetUsers();
            GetRooms();
            RoomList.SelectedIndex = 0;
            Status.SelectedIndex = 0;
        }

        private async void GetUsers()
        {
            LoginModel SavedResponseObj = await helper.ReadFileAsync<LoginModel>("response");

            ConnectionModel IP = new ConnectionModel();
            RestClient client = new RestClient(IP.Adress);
            client.RemoteCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;

            RestRequest request = new RestRequest("Users", Method.GET);
            request.AddParameter("application/json", ParameterType.RequestBody);
            request.AddParameter("Authorization", "Bearer " + SavedResponseObj.Token, ParameterType.HttpHeader);

            // execute the request
            IRestResponse response = client.Execute(request);

            List<GetUsersModel> usersList = new List<GetUsersModel>();
            usersList = JsonConvert.DeserializeAnonymousType(response.Content, usersList);

            UsersList.ItemsSource = usersList;
        }

        private async void GetRooms()
        {
            LoginModel SavedResponseObj = await helper.ReadFileAsync<LoginModel>("response");

            ConnectionModel IP = new ConnectionModel();
            RestClient client = new RestClient(IP.Adress);
            client.RemoteCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;

            RestRequest request = new RestRequest("Room", Method.GET);
            request.AddParameter("application/json", ParameterType.RequestBody);
            request.AddParameter("Authorization", "Bearer " + SavedResponseObj.Token, ParameterType.HttpHeader);

            // execute the request
            IRestResponse response = client.Execute(request);

            List<GetRoomsModel> roomsList = new List<GetRoomsModel>();
            roomsList = JsonConvert.DeserializeAnonymousType(response.Content, roomsList);

            RoomList.ItemsSource = roomsList;
        }

        private void SelectAll_Click(object sender, RoutedEventArgs e)
        {
            if (SelectAll.IsChecked == true)
            {
                UsersList.SelectAll();
            }
            else
            {
                UsersList.SelectedIndex = -1;
            }
        }

        public void UsersList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int[] UsersIndexArray = new int[UsersList.SelectedItems.Count];
            for (int i = 0; i < UsersList.SelectedItems.Count; i++)
            {
                int selectedIndex = UsersList.Items.IndexOf(UsersList.SelectedItems[i]);
                UsersIndexArray[i] = selectedIndex;
            }
        }

        private async void ReservateRoom_Click(object sesnder, RoutedEventArgs e)
        {
            if (TimeFrom.Time != null && DateFrom.Date != null && TimeTo.Time != null && DateTo.Date != null && !string.IsNullOrEmpty(Title.Text) && UsersList.SelectedItems != null)
            {
                LoginModel SavedResponseObj = await helper.ReadFileAsync<LoginModel>("response");

                List<GetUsersModel> UsersIndexArray = new List<GetUsersModel>();
                for (int i = 0; i < UsersList.SelectedItems.Count; i++)
                {
                    int selectedIndex = UsersList.Items.IndexOf(UsersList.SelectedItems[i]);
                    UsersIndexArray[i] = selectedIndex;
                }

                BookModel bookObj = new BookModel
                {
                    //RoomId = RoomList.SelectionBoxItem.ToString;
                    //DateFrom = DateFrom.Date + TimeFrom.Time;
                    //DateTo =
                    Status = Status.Text,
                    Title = Title.Text,
                    //InvitedUsersIds = UsersIndexArray,
                    Token = SavedResponseObj.Token
                };
                string json = JsonConvert.SerializeObject(bookObj);

                ConnectionModel IP = new ConnectionModel();
                RestClient client = new RestClient(IP.Adress);
                client.RemoteCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;

                RestRequest request = new RestRequest("Users", Method.POST);
                request.AddParameter("application/json", json, ParameterType.RequestBody);
                request.AddParameter("Authorization", "Bearer " + SavedResponseObj.Token, ParameterType.HttpHeader);

                // execute the request
                IRestResponse response = client.Execute(request);
                string content = response.Content;

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    await new CustomDialog("Użytkownik stworzony poprawnie.", null, CustomDialog.Type.Information).ShowAsync();
                }
                else
                {
                    await new CustomDialog("Wystąpił błąd podczas komunikacji z serwerem lub użytkownik o podanych danych już istnieje.", null, CustomDialog.Type.Error).ShowAsync();
                }
            }
            else
            {
                await new CustomDialog("Wprowadzono błędne dane.", null, CustomDialog.Type.Warning).ShowAsync();
            }
        }
    }
}