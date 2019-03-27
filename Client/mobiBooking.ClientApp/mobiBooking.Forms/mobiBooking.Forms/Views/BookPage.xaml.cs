using mobiBooking.Core.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace mobiBooking.Forms.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)] ////////// https://xamarinhelp.com/multiselect-listview-xamarin-forms/
    public partial class BookPage : ContentPage
    {
        private readonly ConnectionModel IP = new ConnectionModel();

        private List<GetRoomsModel> roomsList = new List<GetRoomsModel>();
        private List<GetIntervalsModel> intervalsList = new List<GetIntervalsModel>();
        private List<GetUsersModel> usersList = new List<GetUsersModel>();
        private readonly string tokenik = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjEiLCJyb2xlIjoiQWRtaW5pc3RyYXRvciIsInVzZXJOYW1lIjoiVGVzdCIsIm5hbWUiOiJNaWNoYcWCIiwic3VyZU5hbWUiOiJUZXN0IiwiZW1haWwiOiJtLndAZy5wbCIsIm5iZiI6MTU1MzUyNjMyMiwiZXhwIjoxNTU0MTMxMTIyLCJpYXQiOjE1NTM1MjYzMjJ9.AzVLluTv6JqW8VhvZuSkOMA0mXB1teeHp7nTE48HkLM";
        public BookPage()
        {
            InitializeComponent();
        }
        private async Task GetUsers()
        {
            //LoginModel SavedResponseObj = await helper.ReadFileAsync<LoginModel>("response");

            RestClient client = new RestClient(IP.Adress);
            RestRequest request = new RestRequest("Users/get_all", Method.GET);
            _ = request.AddParameter("Authorization", "Bearer " + tokenik, ParameterType.HttpHeader);
            IRestResponse response = client.Execute(request);

            usersList = JsonConvert.DeserializeAnonymousType(response.Content, usersList);
            UsersList.ItemsSource = JsonConvert.DeserializeAnonymousType(response.Content, usersList);
        }

        private async Task PostRooms()
        {
            //LoginModel SavedResponseObj = await helper.ReadFileAsync<LoginModel>("response");

            //DateTime newDateFrom = DateFrom.Date.Value.Date + TimeFrom.Time;
            //string formatedDateFrom = newDateFrom.ToString("yyyy-MM-ddTHH:mm:ss");
            //DateTime newDateTo = DateTo.Date.Value.Date + TimeTo.Time;
            //string formatedDateTo = newDateTo.ToString("yyyy-MM-ddTHH:mm:ss");

            int.TryParse(RoomCap.Text, out int sizeParsesd);

            PostRoomsForRoomsReservationModel roomObj = new PostRoomsForRoomsReservationModel
            {
                Size = sizeParsesd,
                //DateFrom = formatedDateFrom,
                //DateTo = formatedDateTo,
            };
            string json = JsonConvert.SerializeObject(roomObj);

            RestClient client = new RestClient(IP.Adress);
            RestRequest request = new RestRequest("Room/for_reservation", Method.POST);
            _ = request.AddParameter("application/json", json, ParameterType.RequestBody);
            _ = request.AddParameter("Authorization", "Bearer " + tokenik, ParameterType.HttpHeader);
            IRestResponse response = client.Execute(request);

            if (string.IsNullOrEmpty(response.Content))
            {
                //_ = await new CustomDialog("Brak wolnych sal o podanej liczbie miejsc i w danym czasie.", null, CustomDialog.Type.Information).ShowAsync();
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                roomsList = JsonConvert.DeserializeAnonymousType(response.Content, roomsList);
                RoomList.ItemsSource = JsonConvert.DeserializeAnonymousType(response.Content, roomsList);
            }
            else
            {
                // _ = await new CustomDialog("Wystąpił błąd podczas komunikacji z serwerem.", response.StatusCode.ToString(), CustomDialog.Type.Error).ShowAsync();
            }
        }

        private async Task GetIntervals()
        {
            //LoginModel SavedResponseObj = await helper.ReadFileAsync<LoginModel>("response");

            RestClient client = new RestClient(IP.Adress);
            RestRequest request = new RestRequest("Reservation/get_reservation_intervals", Method.GET);
            _ = request.AddParameter("Authorization", "Bearer " + tokenik, ParameterType.HttpHeader);
            IRestResponse response = client.Execute(request);

            intervalsList = JsonConvert.DeserializeAnonymousType(response.Content, intervalsList);
            Intervals.ItemsSource = intervalsList.ToArray();
        }

        private async void ReservateRoom_Click(object sender, EventArgs e)
        {
            SubmitButton.IsEnabled = false;

            if (!string.IsNullOrEmpty(Title.Text)) //TimeFrom.Time != null && DateFrom.Date != null && TimeTo.Time != null && DateTo.Date != null && !string.IsNullOrEmpty(Title.Text) && UsersList.SelectedItems != null)
            {
                //LoginModel SavedResponseObj = await helper.ReadFileAsync<LoginModel>("response");

                //DateTime newDateFrom = DateFrom.Date.Value.Date + TimeFrom.Time;
                //string formatedDateFrom = newDateFrom.ToString("yyyy-MM-ddTHH:mm:ss.924Z");
                //DateTime newDateTo = DateTo.Date.Value.Date + TimeTo.Time;
                //string formatedDateTo = newDateTo.ToString("yyyy-MM-ddTHH:mm:ss.925Z");

                List<int> selectedUsers = new List<int>();
                //foreach (GetUsersModel item in UsersList.SelectedItems)
                //{
                //    selectedUsers.Add(item.Id);
                //}

                BookModel bookObj = new BookModel // TODO FIX/////////////////////////////////////////////////////////////////////////////////////////
                {
                    RoomId = roomsList[RoomList.SelectedIndex].Id,
                    //DateFrom = formatedDateFrom,
                    //DateTo = formatedDateTo,
                    Status = Status.SelectedIndex,
                    Title = Title.Text,
                    InvitedUsersIds = selectedUsers,
                    CyclicReservation = IsCyclic.IsToggled,
                    ReservationIntervalId = intervalsList[Intervals.SelectedIndex].Id
                };
                string json = JsonConvert.SerializeObject(bookObj);

                RestClient client = new RestClient(IP.Adress);
                RestRequest request = new RestRequest("Reservation/create", Method.POST);
                _ = request.AddParameter("application/json", json, ParameterType.RequestBody);
                _ = request.AddParameter("Authorization", "Bearer " + tokenik, ParameterType.HttpHeader);
                IRestResponse response = client.Execute(request);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    //_ = await new CustomDialog("Zarezerwowano salę poprawnie.", null, CustomDialog.Type.Information).ShowAsync();
                    SubmitButton.IsEnabled = true;
                }
                else
                {
                    //_ = await new CustomDialog("Wystąpił błąd podczas komunikacji z serwerem.", response.StatusCode.ToString(), CustomDialog.Type.Error).ShowAsync();
                    SubmitButton.IsEnabled = true;
                }
            }
            else
            {
                //_ = await new CustomDialog("Wprowadzono błędne dane.", null, CustomDialog.Type.Warning).ShowAsync();
                SubmitButton.IsEnabled = true;
            }
        }

        private async void GetRoomList(object sender, EventArgs e)
        {
            //if (TimeFrom.Time != null && DateFrom.Date != null && TimeTo.Time != null && DateTo.Date != null)
            //{
            //    await PostRooms();
            //}
        }

        private async void IsCyclic_Click(object sender, EventArgs e)
        {
            if (IsCyclic.IsToggled)
            {
                Intervals.IsVisible = true;
                await GetIntervals();
            }
            else
            {
                Intervals.IsVisible = false;
            }
        }
    }
}