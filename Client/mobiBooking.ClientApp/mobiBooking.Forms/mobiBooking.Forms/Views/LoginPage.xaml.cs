using Jose;
using mobiBooking.Core.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Net.NetworkInformation;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace mobiBooking.Forms.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private async void SignIn_Button(object sejnder, EventArgs e)
        {
            SubmitButton.IsEnabled = false;

            if (!string.IsNullOrEmpty(email.Text) && !string.IsNullOrEmpty(password.Text) && NetworkInterface.GetIsNetworkAvailable())
            {
                SignInModel loginObj = new SignInModel
                {
                    Email = email.Text,
                    Password = password.Text,
                };
                string json = JsonConvert.SerializeObject(loginObj);

                ConnectionModel IP = new ConnectionModel();
                RestClient client = new RestClient(IP.Adress);
                RestRequest request = new RestRequest("Account/login", Method.POST);
                _ = request.AddParameter("application/json", json, ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);

                if (response.StatusCode != System.Net.HttpStatusCode.InternalServerError && response.StatusCode != System.Net.HttpStatusCode.NotFound && response.StatusCode != System.Net.HttpStatusCode.BadGateway && response.StatusCode != System.Net.HttpStatusCode.BadRequest && response.StatusCode != 0)
                {
                    LoginModel tokenObj = JsonConvert.DeserializeObject<LoginModel>(response.Content);
                    string token = tokenObj.Token;
                    byte[] secretkey = Encoding.UTF8.GetBytes("Secret keyaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");
                    string jsonik = JWT.Decode(token, secretkey);

                    LoginModel DecodedToken = JsonConvert.DeserializeObject<LoginModel>(jsonik);
                    {
                        DecodedToken.Token = tokenObj.Token;
                        DecodedToken.IsLoged = true;
                    };

                    string jsonToSave = JsonConvert.SerializeObject(DecodedToken);

                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        Application.Current.Properties["SavedResponse"] = jsonToSave;
                        await Application.Current.SavePropertiesAsync();

                        SubmitButton.IsEnabled = true;

                        MainPage mainPage = new MainPage();
                        await Navigation.PushModalAsync(mainPage);
                    }
                    else
                    {
                        _ = await DisplayAlert("Błąd", "Wystąpił błąd podczas komunikacji z serwerem.", null, "Ok");
                        SubmitButton.IsEnabled = true;
                    }
                }
                else
                {
                    _ = await DisplayAlert("Błąd", "Konto o podanym loginie/haśle nie istnieje lub brak połączenia z serwerem.", null, "Ok");
                    SubmitButton.IsEnabled = true;
                }
            }
            else
            {
                _ = await DisplayAlert("Ostrzeżenie", "Wprowadzono błędne dane lub brak połączenia z internetem.", null, "Ok");
                SubmitButton.IsEnabled = true;
            }
        }
    }
}