using Microsoft.Toolkit.Uwp.Helpers;
using mobiBooking.UWP.Models;
using mobiBooking.UWP.Views.CustomDialogs;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace mobiBooking.UWP.Views
{
    public sealed partial class BookPage : Page
    {
        private readonly LocalObjectStorageHelper helper = new LocalObjectStorageHelper();
        private readonly ConnectionModel IP = new ConnectionModel();

        private List<GetRoomsModel> roomsList = new List<GetRoomsModel>();
        private List<GetIntervalsModel> intervalsList = new List<GetIntervalsModel>();
        private List<GetUsersModel> usersList = new List<GetUsersModel>();
        private List<BookModel> bookList = new List<BookModel>();
        private List<int> usersListInt = new List<int>();
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
            _ = request.AddParameter("Authorization", "Bearer " + SavedResponseObj.Token, ParameterType.HttpHeader);
            IRestResponse response = client.Execute(request);

            usersList = JsonConvert.DeserializeAnonymousType(response.Content, usersList);
            UsersList.ItemsSource = JsonConvert.DeserializeAnonymousType(response.Content, usersList);
        }

        private async Task PostRooms()
        {
            LoginModel SavedResponseObj = await helper.ReadFileAsync<LoginModel>("response");

            DateTime newDateFrom = DateFrom.Date.Value.Date + TimeFrom.Time;
            string formatedDateFrom = newDateFrom.ToString("yyyy-MM-ddTHH:mm:ss");
            DateTime newDateTo = DateTo.Date.Value.Date + TimeTo.Time;
            string formatedDateTo = newDateTo.ToString("yyyy-MM-ddTHH:mm:ss");

            int.TryParse(RoomCap.Text, out int sizeParsesd);

            PostRoomsForRoomsReservationModel roomObj = new PostRoomsForRoomsReservationModel
            {
                Size = sizeParsesd,
                DateFrom = formatedDateFrom,
                DateTo = formatedDateTo,
            };
            string json = JsonConvert.SerializeObject(roomObj);

            RestClient client = new RestClient(IP.Adress);
            RestRequest request = new RestRequest("Room/for_reservation", Method.POST);
            _ = request.AddParameter("application/json", json, ParameterType.RequestBody);
            _ = request.AddParameter("Authorization", "Bearer " + SavedResponseObj.Token, ParameterType.HttpHeader);
            IRestResponse response = client.Execute(request);

            if (string.IsNullOrEmpty(response.Content))
            {
                _ = await new CustomDialog("Brak wolnych sal.", null, CustomDialog.Type.Information).ShowAsync();
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                roomsList = JsonConvert.DeserializeAnonymousType(response.Content, roomsList);
                RoomList.ItemsSource = JsonConvert.DeserializeAnonymousType(response.Content, roomsList);
            }
            else
            {
            }
        }

        private async Task GetIntervals()
        {
            LoginModel SavedResponseObj = await helper.ReadFileAsync<LoginModel>("response");

            RestClient client = new RestClient(IP.Adress);
            RestRequest request = new RestRequest("Reservation/get_reservation_intervals", Method.GET);
            _ = request.AddParameter("Authorization", "Bearer " + SavedResponseObj.Token, ParameterType.HttpHeader);
            IRestResponse response = client.Execute(request);

            intervalsList = JsonConvert.DeserializeAnonymousType(response.Content, intervalsList);
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

        private async void ReservateRoom_Click(object sender, RoutedEventArgs e)
        {
            SubmitButton.IsEnabled = false;

            if (TimeFrom.Time != null && DateFrom.Date != null && TimeTo.Time != null && DateTo.Date != null && !string.IsNullOrEmpty(Title.Text) && UsersList.SelectedItems != null)
            {
                LoginModel SavedResponseObj = await helper.ReadFileAsync<LoginModel>("response");

                DateTime newDateFrom = DateFrom.Date.Value.Date + TimeFrom.Time;
                string formatedDateFrom = newDateFrom.ToString("yyyy-MM-ddTHH:mm:ss.924Z");
                DateTime newDateTo = DateTo.Date.Value.Date + TimeTo.Time;
                string formatedDateTo = newDateTo.ToString("yyyy-MM-ddTHH:mm:ss.925Z");

                bookList.ForEach(CoutInvitedUsers);

                BookModel bookObj = new BookModel // TODO FIX/////////////////////////////////////////////////////////////////////////////////////////
                {
                    RoomId = roomsList[RoomList.SelectedIndex].Id,
                    DateFrom = formatedDateFrom,
                    DateTo = formatedDateTo,
                    //Status = Status.SelectedIndex + 1,
                    Title = Title.Text,
                    InvitedUsersIds = usersListInt,
                    CyclicReservation = IsCyclic.IsChecked ?? false,
                    ReservationIntervalId = intervalsList[Intervals.SelectedIndex].Id
                };
                string json = JsonConvert.SerializeObject(bookObj);

                RestClient client = new RestClient(IP.Adress);
                RestRequest request = new RestRequest("Reservation/create", Method.POST);
                _ = request.AddParameter("application/json", json, ParameterType.RequestBody);
                _ = request.AddParameter("Authorization", "Bearer " + SavedResponseObj.Token, ParameterType.HttpHeader);
                IRestResponse response = client.Execute(request);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    _ = await new CustomDialog("Zarezerwowano salę poprawnie.", null, CustomDialog.Type.Information).ShowAsync();
                    SubmitButton.IsEnabled = true;
                }
                else
                {
                    _ = await new CustomDialog("Wystąpił błąd podczas komunikacji z serwerem.", response.StatusCode.ToString(), CustomDialog.Type.Error).ShowAsync();
                    SubmitButton.IsEnabled = true;
                }
            }
            else
            {
                _ = await new CustomDialog("Wprowadzono błędne dane.", null, CustomDialog.Type.Warning).ShowAsync();
                SubmitButton.IsEnabled = true;
            }
        }

        private void CoutInvitedUsers(BookModel obj)
        {
            obj.usersListInt.Add(usersList[UsersList.SelectedIndex].Id);
        }

        private async void GetRoomList(object sender, RoutedEventArgs e)
        {
            await PostRooms();
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