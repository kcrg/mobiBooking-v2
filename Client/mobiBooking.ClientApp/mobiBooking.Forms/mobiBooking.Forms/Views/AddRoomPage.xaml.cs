
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
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddRoomPage : ContentPage
    {
        private readonly ConnectionModel IP = new ConnectionModel();

        string tokenik = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjEiLCJyb2xlIjoiQWRtaW5pc3RyYXRvciIsInVzZXJOYW1lIjoiVGVzdCIsIm5hbWUiOiJNaWNoYcWCIiwic3VyZU5hbWUiOiJUZXN0IiwiZW1haWwiOiJtLndAZy5wbCIsIm5iZiI6MTU1MzUyNjMyMiwiZXhwIjoxNTU0MTMxMTIyLCJpYXQiOjE1NTM1MjYzMjJ9.AzVLluTv6JqW8VhvZuSkOMA0mXB1teeHp7nTE48HkLM";
        public AddRoomPage()
        {
            InitializeComponent();

            GetIntervals();
        }
        private async Task GetIntervals()
        {
            //LoginModel SavedResponseObj = await helper.ReadFileAsync<LoginModel>("response");

            RestClient client = new RestClient(IP.Adress);
            RestRequest request = new RestRequest("Room/get_room_availabilities", Method.GET);
            _ = request.AddParameter("Authorization", "Bearer " + tokenik, ParameterType.HttpHeader);
            IRestResponse response = client.Execute(request);

            List<GetIntervalsModel> availabilityList = new List<GetIntervalsModel>();
            availability.ItemsSource = JsonConvert.DeserializeAnonymousType(response.Content, availabilityList);
            availability.SelectedIndex = 0;
        }

        private async void Add_Click(object senjder, EventArgs e)
        {
            SubmitButton.IsEnabled = false;

            if (!string.IsNullOrEmpty(roomname.Text) && !string.IsNullOrEmpty(localization.Text) && !string.IsNullOrEmpty(numberofpeople.Text))
            {
                //LoginModel SavedResponseObj = await helper.ReadFileAsync<LoginModel>("response");

                AddRoomModel roomObj = new AddRoomModel
                {
                    RoomName = roomname.Text.Trim(),
                    Location = localization.Text.Trim(),
                    Availability = availability.SelectedIndex + 1,
                    NumberOfPeople = Convert.ToInt32(numberofpeople.Text)
                };
                string json = JsonConvert.SerializeObject(roomObj);

                RestClient client = new RestClient(IP.Adress);
                RestRequest request = new RestRequest("Room/create", Method.POST);
                _ = request.AddParameter("application/json", json, ParameterType.RequestBody);
                _ = request.AddParameter("Authorization", "Bearer " + tokenik, ParameterType.HttpHeader);
                IRestResponse response = client.Execute(request);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    //_ = await new CustomDialog("Sala dodana poprawnie.", null, CustomDialog.Type.Information).ShowAsync();
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
    }
}