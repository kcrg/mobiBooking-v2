using Microsoft.Toolkit.Uwp.Helpers;
using mobiBooking.UWP.Models;
using mobiBooking.UWP.Views.CustomDialogs;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace mobiBooking.UWP.Views
{
    public sealed partial class BookPage : Page
    {
        private readonly LocalObjectStorageHelper helper = new LocalObjectStorageHelper();
        private readonly ConnectionModel IP = new ConnectionModel();
        public BookPage()
        {
            InitializeComponent();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await GetUsers();
            Status.SelectedIndex = 0;
            Intervals.SelectedIndex = 0;
        }

        private async Task GetUsers()
        {
            LoginModel SavedResponseObj = await helper.ReadFileAsync<LoginModel>("response");

            RestClient client = new RestClient(IP.Adress);
            RestRequest request = new RestRequest("Users/get_all", Method.GET);
            request.AddParameter("Authorization", "Bearer " + SavedResponseObj.Token, ParameterType.HttpHeader);

            // execute the request
            IRestResponse response = client.Execute(request);

            List<GetUsersModel> usersList = new List<GetUsersModel>();

            UsersList.ItemsSource = JsonConvert.DeserializeAnonymousType(response.Content, usersList);
        }

        private async Task GetRooms()
        {
            LoginModel SavedResponseObj = await helper.ReadFileAsync<LoginModel>("response");

            RestClient client = new RestClient(IP.Adress);
            RestRequest request = new RestRequest("Room/get_all", Method.GET);
            request.AddParameter("Authorization", "Bearer " + SavedResponseObj.Token, ParameterType.HttpHeader);

            // execute the request
            IRestResponse response = client.Execute(request);

            List<GetRoomsModel> roomsList = new List<GetRoomsModel>();

            RoomList.ItemsSource = JsonConvert.DeserializeAnonymousType(response.Content, roomsList);
            RoomList.SelectedIndex = 0;
        }

        private async Task GetIntervals()
        {
            LoginModel SavedResponseObj = await helper.ReadFileAsync<LoginModel>("response");

            RestClient client = new RestClient(IP.Adress);
            RestRequest request = new RestRequest("Reservation/get_reservation_intervals", Method.GET);
            request.AddParameter("Authorization", "Bearer " + SavedResponseObj.Token, ParameterType.HttpHeader);

            // execute the request
            IRestResponse response = client.Execute(request);

            List<GetIntervalsModel> intervalsList = new List<GetIntervalsModel>();

            Intervals.ItemsSource = JsonConvert.DeserializeAnonymousType(response.Content, intervalsList);
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

                BookModel bookObj = new BookModel // TODO FIX/////////////////////////////////////////////////////////////////////////////////////////
                {
                    RoomId = RoomList.SelectedIndex + 1,
                    DateFrom = formatedDateFrom,
                    DateTo = formatedDateTo,
                    Status = Status.SelectedIndex + 1,
                    Title = Title.Text,
                    InvitedUsersIds = UsersIndexArray.Select(s => s + 1).AsEnumerable().ToList(),
                    CyclicReservation = IsCyclic.IsChecked ?? false,
                    ReservationIntervalId = Intervals.SelectedIndex + 1
                };
                string json = JsonConvert.SerializeObject(bookObj);

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

        private async void Flipchart_Click(object sender, RoutedEventArgs e)
        {
            await GetRooms();
        }

        private async void SoundSystem_Click(object sender, RoutedEventArgs e)
        {
            await GetRooms();
        }

        private async void RoomCap_LostFocus(object sender, RoutedEventArgs e)
        {
            await GetRooms();
        }

        private async void IsCyclic_Click(object sender, RoutedEventArgs e)
        {
            if (IsCyclic.IsChecked ?? false)
            {
                Intervals.Visibility = Visibility.Visible;
                await GetIntervals();
            }
            else
            {
                Intervals.Visibility = Visibility.Collapsed;
            }
        }
    }
}