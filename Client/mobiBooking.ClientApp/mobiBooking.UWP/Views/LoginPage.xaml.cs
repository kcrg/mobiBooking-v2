using Microsoft.IdentityModel.Tokens;
using Microsoft.Toolkit.Uwp.Helpers;
using Microsoft.Toolkit.Uwp.UI.Extensions;
using mobiBooking.UWP.ViewModels;
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
            if (!string.IsNullOrEmpty(password.Password) && TextBoxRegex.GetIsValid(email) && NetworkInterface.GetIsNetworkAvailable())
            {
                SignInModel loginObj = new SignInModel
                {
                    Email = email.Text,
                    Password = password.Password,
                };
                string json = JsonConvert.SerializeObject(loginObj);
                Console.WriteLine(json);

                ConnectionModel IP = new ConnectionModel();
                RestClient client = new RestClient(IP.Adress);
                client.RemoteCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;

                RestRequest request = new RestRequest("Authenticate", Method.POST);
                request.AddParameter("application/json", json, ParameterType.RequestBody);

                // execute the request
                IRestResponse response = client.Execute(request);
                string content = response.Content; // raw content as string

                LoginViewModel tokenObj = new LoginViewModel();
                tokenObj = JsonConvert.DeserializeObject<LoginViewModel>(response.Content);

                if (tokenObj.Token != null)
                {
                    JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
                    SecurityToken jsonToken = handler.ReadToken(tokenObj.Token);

                    LoginViewModel tokenvalidateObj = new LoginViewModel
                    {
                        UserName = ((JwtSecurityToken)jsonToken).Payload["userName"].ToString(),
                        Name = ((JwtSecurityToken)jsonToken).Payload["name"].ToString(),
                        Surname = ((JwtSecurityToken)jsonToken).Payload["sureName"].ToString(),
                        Email = ((JwtSecurityToken)jsonToken).Payload["email"].ToString(),
                        UserType = ((JwtSecurityToken)jsonToken).Payload["role"].ToString(),
                        Token = tokenObj.Token
                    };

                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        await helper.SaveFileAsync("response", tokenvalidateObj);

                        Frame.Navigate(typeof(MainPage), null, new DrillInNavigationTransitionInfo());
                    }
                    else
                    {
                        await new CustomDialog("Wystąpił błąd podczas komunikacji z serwerem.", CustomDialog.Type.Error).ShowAsync();
                    }
                }
                else
                {
                    await new CustomDialog("Konto o podanym loginie/haśle nie istnieje.", CustomDialog.Type.Warning).ShowAsync();
                }
            }
            else
            {
                await new CustomDialog("Wprowadzono błędne dane lub brak połączenia z internetem.", CustomDialog.Type.Warning).ShowAsync();
            }
        }
    }
}