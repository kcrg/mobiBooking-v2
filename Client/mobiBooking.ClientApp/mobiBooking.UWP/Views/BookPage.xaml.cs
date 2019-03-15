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
            RestRequest request = new RestRequest("Users/all_for_reservation", Method.GET);
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
            RestRequest request = new RestRequest("Room/get_all", Method.GET);
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

        private async void ReservateRoom_Click(object sesnder, RoutedEventArgs e)
        {
            SubmitButton.IsEnabled = false;

            if (TimeFrom.Time != null && DateFrom.Date != null && TimeTo.Time != null && DateTo.Date != null && !string.IsNullOrEmpty(Title.Text) && UsersList.SelectedItems != null)
            {
                LoginModel SavedResponseObj = await helper.ReadFileAsync<LoginModel>("response");

                List<int> UsersIndexArray = new List<int>();
                for (int i = 0; i < UsersList.SelectedItems.Count; i++)
                {
                    int selectedIndex = UsersList.Items.IndexOf(UsersList.SelectedItems[i]);
                    UsersIndexArray.Add(selectedIndex);
                }

                DateTime newDateFrom = DateFrom.Date.Value.Date + TimeFrom.Time;
                string formatedDateFrom = newDateFrom.ToString("yyyy-MM-ddTHH:mm:ss.924Z");

                DateTime newDateTo = DateTo.Date.Value.Date + TimeTo.Time;
                string formatedDateTo = newDateTo.ToString("yyyy-MM-ddTHH:mm:ss.925Z");


                BookModel bookObj = new BookModel
                {
                    RoomId = RoomList.SelectedIndex,
                    DateFrom = formatedDateFrom,
                    DateTo = formatedDateTo,
                    Status = Status.SelectedItem.ToString(),
                    Title = Title.Text,
                    InvitedUsersIds = UsersIndexArray
                };
                string json = JsonConvert.SerializeObject(bookObj);

                ConnectionModel IP = new ConnectionModel();
                RestClient client = new RestClient(IP.Adress);
                RestRequest request = new RestRequest("Reservation/create", Method.POST);
                request.AddParameter("application/json", json, ParameterType.RequestBody);
                request.AddParameter("Authorization", "Bearer " + SavedResponseObj.Token, ParameterType.HttpHeader);

                // execute the request
                IRestResponse response = client.Execute(request);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    await new CustomDialog("Zarezerwowano salę poprawnie.", null, CustomDialog.Type.Information).ShowAsync();
                    SubmitButton.IsEnabled = true;
                }
                else
                {
                    await new CustomDialog("Wystąpił błąd podczas komunikacji z serwerem.", response.StatusCode.ToString(), CustomDialog.Type.Error).ShowAsync();
                    SubmitButton.IsEnabled = true;
                }
            }
            else
            {
                await new CustomDialog("Wprowadzono błędne dane.", null, CustomDialog.Type.Warning).ShowAsync();
                SubmitButton.IsEnabled = true;
            }
        }
    }
}