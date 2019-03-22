using Microsoft.IdentityModel.Tokens;
using Microsoft.Toolkit.Uwp.Helpers;
using Microsoft.Toolkit.Uwp.UI.Extensions;
using mobiBooking.UWP.Models;
using mobiBooking.UWP.Views.CustomDialogs;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net.NetworkInformation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;

namespace mobiBooking.UWP.Views
{
    public sealed partial class LoginPage : Page
    {
        private readonly LocalObjectStorageHelper helper = new LocalObjectStorageHelper();
        public LoginPage()
        {
            InitializeComponent();
        }

        private async void SignIn_Button(object sejnder, RoutedEventArgs e)
        {
            SubmitButton.IsEnabled = false;

            if (!string.IsNullOrEmpty(password.Password) && TextBoxRegex.GetIsValid(email) && NetworkInterface.GetIsNetworkAvailable())
            {
                SignInModel loginObj = new SignInModel
                {
                    Email = email.Text,
                    Password = password.Password,
                };
                string json = JsonConvert.SerializeObject(loginObj);

                ConnectionModel IP = new ConnectionModel();
                RestClient client = new RestClient(IP.Adress);
                RestRequest request = new RestRequest("Account/login", Method.POST);
                _ = request.AddParameter("application/json", json, ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);

                if (response.StatusCode != System.Net.HttpStatusCode.InternalServerError && response.StatusCode != System.Net.HttpStatusCode.NotFound && response.StatusCode != System.Net.HttpStatusCode.BadGateway && response.StatusCode != System.Net.HttpStatusCode.BadRequest && response.StatusCode != 0)
                {
                    _ = new LoginModel();
                    LoginModel tokenObj = JsonConvert.DeserializeObject<LoginModel>(response.Content);

                    JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
                    SecurityToken jsonToken = handler.ReadToken(tokenObj.Token);

                    LoginModel tokenvalidateObj = new LoginModel
                    {
                        UserName = ((JwtSecurityToken)jsonToken).Payload["userName"].ToString(),
                        Name = ((JwtSecurityToken)jsonToken).Payload["name"].ToString(),
                        Surname = ((JwtSecurityToken)jsonToken).Payload["sureName"].ToString(),
                        Email = ((JwtSecurityToken)jsonToken).Payload["email"].ToString(),
                        UserType = ((JwtSecurityToken)jsonToken).Payload["role"].ToString(),
                        Token = tokenObj.Token,
                        IsLoged = true
                    };

                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        _ = await helper.SaveFileAsync("response", tokenvalidateObj);
                        SubmitButton.IsEnabled = true;

                        _ = Frame.Navigate(typeof(MainPage), null, new DrillInNavigationTransitionInfo());
                    }
                    else
                    {
                        _ = await new CustomDialog("Wystąpił błąd podczas komunikacji z serwerem.", response.StatusCode.ToString(), CustomDialog.Type.Error).ShowAsync();
                        SubmitButton.IsEnabled = true;
                    }
                }
                else
                {
                    _ = await new CustomDialog("Konto o podanym loginie/haśle nie istnieje lub brak połączenia z serwerem.", response.StatusCode.ToString(), CustomDialog.Type.Error).ShowAsync();
                    SubmitButton.IsEnabled = true;
                }
            }
            else
            {
                _ = await new CustomDialog("Wprowadzono błędne dane lub brak połączenia z internetem.", null, CustomDialog.Type.Warning).ShowAsync();
                SubmitButton.IsEnabled = true;
            }
        }
    }
}