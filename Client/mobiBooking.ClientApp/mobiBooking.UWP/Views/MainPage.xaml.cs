using mobiBooking.UWP.Views;
using Newtonsoft.Json;
using RestSharp;
using Windows.ApplicationModel.Core;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace mobiBooking.UWP
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();

            CoreApplicationViewTitleBar CoreTitleBar = CoreApplication.GetCurrentView().TitleBar;
            CoreTitleBar.ExtendViewIntoTitleBar = true;

            Window.Current.SetTitleBar(DragArea);

            ApplicationViewTitleBar titleBar = ApplicationView.GetForCurrentView().TitleBar;
            titleBar.ButtonBackgroundColor = Colors.Transparent;
            titleBar.ButtonInactiveBackgroundColor = Colors.Transparent;

            ContentFrame.Navigate(typeof(DashboardPage));
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            var parameters = (ResponseClass)e.Parameter;
        }

        private void LogOut_Click(object sejnder, RoutedEventArgs e)
        {
            ResponseClass objResponse = new ResponseClass();
            string json = JsonConvert.SerializeObject(objResponse.Token);


            RestClient client = new RestClient("http://192.168.10.240:51290/api");
            // client.Authenticator = new HttpBasicAuthenticator(username, password);
            client.RemoteCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;

            RestRequest request = new RestRequest("Authenticate/Logout", Method.POST);
            request.AddParameter("Authorization", "Bearer " + objResponse.Token, ParameterType.HttpHeader);

            // execute the request
            IRestResponse response = client.Execute(request);
            string content = response.Content; // raw content as string

            Frame.Navigate(typeof(LoginPage));
        }
    }
}