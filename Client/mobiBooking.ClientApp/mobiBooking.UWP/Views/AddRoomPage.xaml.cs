using Microsoft.Toolkit.Uwp.Helpers;
using Microsoft.Toolkit.Uwp.UI.Extensions;
using mobiBooking.UWP.Models;
using mobiBooking.UWP.Views.CustomDialogs;
using Newtonsoft.Json;
using RestSharp;
using System;
using Windows.UI.Xaml.Controls;

namespace mobiBooking.UWP.Views
{
    public sealed partial class AddRoomPage : Page
    {
        private readonly LocalObjectStorageHelper helper = new LocalObjectStorageHelper();
        public AddRoomPage()
        {
            InitializeComponent();

            availability.SelectedIndex = 0;
        }

        private async void Add_Click(object senjder, Windows.UI.Xaml.RoutedEventArgs e)
        {
            SubmitButton.IsEnabled = false;

            if (!string.IsNullOrEmpty(roomname.Text) && !string.IsNullOrEmpty(localization.Text) && TextBoxRegex.GetIsValid(numberofpeople))
            {
                LoginModel SavedResponseObj = await helper.ReadFileAsync<LoginModel>("response");

                AddRoomModel roomObj = new AddRoomModel
                {
                    RoomName = roomname.Text,
                    Location = localization.Text,
                    Activity = activity.IsChecked ?? false,
                    Availability = availability.SelectedItem.ToString(),
                    NumberOfPeople = Convert.ToInt32(numberofpeople.Text),
                    Token = SavedResponseObj.Token
                };
                string json = JsonConvert.SerializeObject(roomObj);

                ConnectionModel IP = new ConnectionModel();
                RestClient client = new RestClient(IP.Adress);
                RestRequest request = new RestRequest("Room/create", Method.POST);
                request.AddParameter("application/json", json, ParameterType.RequestBody);
                request.AddParameter("Authorization", "Bearer " + SavedResponseObj.Token, ParameterType.HttpHeader);

                // execute the request
                IRestResponse response = client.Execute(request);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    await new CustomDialog("Sala dodana poprawnie.", null, CustomDialog.Type.Information).ShowAsync();
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