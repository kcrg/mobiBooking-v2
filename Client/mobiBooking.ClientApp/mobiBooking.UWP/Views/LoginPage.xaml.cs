using Microsoft.Toolkit.Uwp.UI.Extensions;
using mobiBooking.UWP.ViewModels;
using mobiBooking.UWP.Views.CustomDialogs;
using Newtonsoft.Json;
using RestSharp;
using System;
using Windows.ApplicationModel.Core;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;

namespace mobiBooking.UWP.Views
{
    public sealed partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();

            CoreApplicationViewTitleBar CoreTitleBar = CoreApplication.GetCurrentView().TitleBar;
            CoreTitleBar.ExtendViewIntoTitleBar = true;
            ApplicationViewTitleBar titleBar = ApplicationView.GetForCurrentView().TitleBar;
            titleBar.ButtonBackgroundColor = Colors.Transparent;
            titleBar.ButtonInactiveBackgroundColor = Colors.Transparent;
        }

        private async void SignIn_Button(object sejnder, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(password.Password) && TextBoxRegex.GetIsValid(email))
            {
                //SignInModel loginObj = new SignInModel
                //{
                //    Email = email.Text,
                //    Password = password.Password,
                //};
                //string json = JsonConvert.SerializeObject(loginObj);
                //Console.WriteLine(json);


                //RestClient client = new RestClient("http://192.168.10.240:51290/api");
                //// client.Authenticator = new HttpBasicAuthenticator(username, password);
                //client.RemoteCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;

                //RestRequest request = new RestRequest("Authenticate", Method.POST);
                //request.AddParameter("application/json", json, ParameterType.RequestBody);

                //// execute the request
                //IRestResponse response = client.Execute(request);
                //string content = response.Content; // raw content as string

                //LoginViewModel responseObj = new LoginViewModel();
                //responseObj = JsonConvert.DeserializeObject<LoginViewModel>(response.Content);

                //if (response.StatusCode == System.Net.HttpStatusCode.OK)
                //{
                    Frame.Navigate(typeof(MainPage), null, new DrillInNavigationTransitionInfo());
                //}
                //else
                //{
                //    ContentDialogResult error = await new ErrorDialog().ShowAsync();
                //}
            }
            else
            {
                ContentDialogResult error = await new ErrorDialog().ShowAsync();
            }
        }
    }
}