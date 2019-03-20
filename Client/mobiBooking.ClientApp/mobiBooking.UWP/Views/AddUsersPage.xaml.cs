using Microsoft.Toolkit.Uwp.Helpers;
using Microsoft.Toolkit.Uwp.UI.Extensions;
using mobiBooking.UWP.Models;
using mobiBooking.UWP.Views.CustomDialogs;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Threading.Tasks;
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
            await AddUser();
        }

        private async Task AddUser()
        {
            SubmitButton.IsEnabled = false;

            if (!string.IsNullOrEmpty(username.Text) && !string.IsNullOrEmpty(password.Password) && !string.IsNullOrEmpty(passwordconfirm.Password) && password.Password == passwordconfirm.Password && TextBoxRegex.GetIsValid(email))
            {
                LoginModel SavedResponseObj = await helper.ReadFileAsync<LoginModel>("response");

                AddUserModel userObj = new AddUserModel
                {
                    UserName = username.Text.Trim(),
                    Password = password.Password.Trim(),
                    Name = name.Text.Trim(),
                    Surname = surname.Text.Trim(),
                    Email = email.Text.Trim(),
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

                    // clean user input
                    username.Text = string.Empty;
                    password.Password = string.Empty;
                    passwordconfirm.Password = string.Empty;
                    name.Text = string.Empty;
                    surname.Text = string.Empty;
                    email.Text = string.Empty;

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