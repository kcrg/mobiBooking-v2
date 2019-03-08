using Newtonsoft.Json;
using RestSharp;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace mobiBooking.UWP.Views
{
    public sealed partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void SignIn_Button(object sejnder, RoutedEventArgs e)
        {
            Login obj = new Login
            {
                Email = email.Text,
                Password = password.Text,
            };
            string json = JsonConvert.SerializeObject(obj);
            Console.WriteLine(json);


            RestClient client = new RestClient("http://192.168.10.240:51290/api");
            // client.Authenticator = new HttpBasicAuthenticator(username, password);
            client.RemoteCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;

            RestRequest request = new RestRequest("Authenticate", Method.POST);
            request.AddParameter("application/json", json, ParameterType.RequestBody);

            // execute the request
            IRestResponse response = client.Execute(request);
            string content = response.Content; // raw content as string

            ResponseClass responseObj = new ResponseClass();
            responseObj = JsonConvert.DeserializeObject<ResponseClass>(response.Content);

            if (responseObj.Token != null)
            {
                Frame.Navigate(typeof(MainPage), responseObj);
            }
        }
    }

    internal class Login
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
