using Microsoft.Toolkit.Uwp.Helpers;
using Microsoft.Toolkit.Uwp.UI.Extensions;
using mobiBooking.UWP.Models;
using mobiBooking.UWP.Views.CustomDialogs;
using Newtonsoft.Json;
using RestSharp;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace mobiBooking.UWP.Views
{
    public sealed partial class AddUsersPage : Page
    {
        private readonly LocalObjectStorageHelper helper = new LocalObjectStorageHelper();
        public AddUsersPage()
        {
            InitializeComponent();
            usertype.SelectedIndex = 0;
        }

        private async void Add_Click(object senjder, RoutedEventArgs e)
        {
            SubmitButton.IsEnabled = false;

            if (!string.IsNullOrEmpty(username.Text) && !string.IsNullOrEmpty(password.Password) && !string.IsNullOrEmpty(passwordconfirm.Password) && password.Password == passwordconfirm.Password && TextBoxRegex.GetIsValid(email))
            {
                LoginModel SavedResponseObj = await helper.ReadFileAsync<LoginModel>("response");

                AddUserModel userObj = new AddUserModel
                {
                    UserName = username.Text,
                    Password = password.Password,
                    Name = name.Text,
                    Surname = surname.Text,
                    Email = email.Text,
                    UserType = usertype.SelectedItem.ToString()
                };
                string json = JsonConvert.SerializeObject(userObj);

                ConnectionModel IP = new ConnectionModel();
                RestClient client = new RestClient(IP.Adress);
                RestRequest request = new RestRequest("Account/create", Method.POST);
                request.AddParameter("application/json", json, ParameterType.RequestBody);
                request.AddParameter("Authorization", "Bearer " + SavedResponseObj.Token, ParameterType.HttpHeader);

                // execute the request
                IRestResponse response = client.Execute(request);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    await new CustomDialog("Użytkownik stworzony poprawnie.", null, CustomDialog.Type.Information).ShowAsync();
                    SubmitButton.IsEnabled = true;
                }
                else
                {
                    await new CustomDialog("Wystąpił błąd podczas komunikacji z serwerem lub użytkownik o podanych danych już istnieje.", response.StatusCode.ToString(), CustomDialog.Type.Error).ShowAsync();
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