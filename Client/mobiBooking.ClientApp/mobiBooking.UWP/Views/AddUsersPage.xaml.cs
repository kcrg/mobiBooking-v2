using mobiBooking.UWP.ViewModels;
using mobiBooking.UWP.Views.CustomDialogs;
using Newtonsoft.Json;
using RestSharp;
using System;
using Windows.UI.Xaml.Controls;

namespace mobiBooking.UWP.Views
{
    public sealed partial class AddUsersPage : Page
    {
        public AddUsersPage()
        {
            InitializeComponent();
        }

        private async void Add_Click(object senjder, Windows.UI.Xaml.RoutedEventArgs e)
        {
            AddUserViewModel userObj = new AddUserViewModel
            {
                UserName = username.Text,
                Password = password.Text,
                Name = name.Text,
                Surname = surname.Text,
                Email = email.Text,
                UserType = usertype.SelectedItem.ToString(),
                //Token = 
            };
            string json = JsonConvert.SerializeObject(userObj);
            Console.WriteLine(json);


            RestClient client = new RestClient("http://192.168.10.240:51290/api");
            client.RemoteCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;

            RestRequest request = new RestRequest("Users", Method.POST);
            request.AddParameter("application/json", json, ParameterType.RequestBody);

            // execute the request
            IRestResponse response = client.Execute(request);

            LoginViewModel responseObj = new LoginViewModel();
            responseObj = JsonConvert.DeserializeObject<LoginViewModel>(response.Content);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {

            }
            else
            {
                ContentDialogResult error = await new ErrorDialog().ShowAsync();
            }
        }
    }
}