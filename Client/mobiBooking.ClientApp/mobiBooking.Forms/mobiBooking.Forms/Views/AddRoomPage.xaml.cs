
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
        private LoginModel SavedLoginObj = new LoginModel();
        public AddRoomPage()
        {
            InitializeComponent();

            string SavedLoginJson = Application.Current.Properties["SavedResponse"].ToString();
            SavedLoginObj = JsonConvert.DeserializeObject<LoginModel>(SavedLoginJson);

            GetIntervals();
        }
        private async Task GetIntervals()
        {
            RestClient client = new RestClient(IP.Adress);
            RestRequest request = new RestRequest("Room/get_room_availabilities", Method.GET);
            _ = request.AddParameter("Authorization", "Bearer " + SavedLoginObj.Token, ParameterType.HttpHeader);
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
                _ = request.AddParameter("Authorization", "Bearer " + SavedLoginObj.Token, ParameterType.HttpHeader);
                IRestResponse response = client.Execute(request);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    _ = await DisplayAlert("Informacja", "Sala została dodana poprawnie.", null, "Ok");
                    SubmitButton.IsEnabled = true;
                }
                else
                {
                    _ = await DisplayAlert("Błąd", "Wystąpił błąd podczas komunikacji z serwerem.", null, "Ok");
                    SubmitButton.IsEnabled = true;
                }
            }
            else
            {
                _ = await DisplayAlert("Ostrzeżenie", "Wprowadzono błędne dane.", null, "Ok");
                SubmitButton.IsEnabled = true;
            }
        }
    }
}