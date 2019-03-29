using Microsoft.Toolkit.Uwp.Helpers;
using Microsoft.Toolkit.Uwp.UI.Extensions;
using mobiBooking.Core.Models;
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
    public sealed partial class AddRoomPage : Page
    {
        private readonly LocalObjectStorageHelper helper = new LocalObjectStorageHelper();
        private readonly ConnectionModel IP = new ConnectionModel();
        private bool IsEditMode;
        public AddRoomPage()
        {
            InitializeComponent();
        }

        protected async override void OnNavigatedTo(Windows.UI.Xaml.Navigation.NavigationEventArgs e)
        {
            CleanupInput();
            SubmitButton.Content = "Dodaj użytkownika";

            if (e.Parameter != null)
            {
                await GetIntervals();

                GetRoomsModel m = JsonConvert.DeserializeObject<GetRoomsModel>((string)e.Parameter);

                IsEditMode = true;

                SubmitButton.Content = "Edytuj salę";

                roomname.Text = m.RoomName;
                localization.Text = m.Location;
                availability.SelectedIndex = m.AvailabilityId;
                numberofpeople.Text = m.NumberOfPeople.ToString();

            }
            base.OnNavigatedTo(e);
        }

        private async Task GetIntervals()
        {
            LoginModel SavedResponseObj = await helper.ReadFileAsync<LoginModel>("response");

            RestClient client = new RestClient(IP.Adress);
            RestRequest request = new RestRequest("Room/get_room_availabilities", Method.GET);
            _ = request.AddParameter("Authorization", "Bearer " + SavedResponseObj.Token, ParameterType.HttpHeader);
            IRestResponse response = client.Execute(request);

            List<GetIntervalsModel> availabilityList = new List<GetIntervalsModel>();
            availability.ItemsSource = JsonConvert.DeserializeAnonymousType(response.Content, availabilityList);
            if (!IsEditMode)
            {
                availability.SelectedIndex = 0;
            }
        }

        private async void Add_Click(object senjder, RoutedEventArgs e)
        {
            if (IsEditMode)
            {
                await AddUser("Room/update/{id}", Method.PUT);
            }
            else
            {
                await AddUser("Room/create", Method.POST);
            }
        }

        private async Task AddUser(string resource, Method method)
        {
            SubmitButton.IsEnabled = false;

            if (!string.IsNullOrEmpty(roomname.Text) && !string.IsNullOrEmpty(localization.Text) && TextBoxRegex.GetIsValid(numberofpeople))
            {
                LoginModel SavedResponseObj = await helper.ReadFileAsync<LoginModel>("response");

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
                _ = request.AddParameter("Authorization", "Bearer " + SavedResponseObj.Token, ParameterType.HttpHeader);
                IRestResponse response = client.Execute(request);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    _ = await new CustomDialog("Sala dodana poprawnie.", null, CustomDialog.Type.Information).ShowAsync();
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

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (!IsEditMode)
            {
                await GetIntervals();
            }       
        }

        private void CleanupInput()
        {
            //username.Text = string.Empty;
            //password.Password = string.Empty;
            //passwordconfirm.Password = string.Empty;
            //name.Text = string.Empty;
            //surname.Text = string.Empty;
            //email.Text = string.Empty;

            //IsEditMode = false;
            //UserID = 0;
            //activity.Visibility = Visibility.Collapsed;
            //activity.IsChecked = true;
        }
    }
}