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
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

namespace mobiBooking.UWP.Views
{
    public sealed partial class AddUsersPage : Page
    {
        private readonly LocalObjectStorageHelper helper = new LocalObjectStorageHelper();
        private readonly ConnectionModel IP = new ConnectionModel();
        private bool IsEditMode;
        private int UserID;
        public AddUsersPage()
        {
            InitializeComponent();

            usertype.SelectedIndex = 0;
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            CleanupInput();
            SubmitButton.Content = "Dodaj użytkownika";

            if (e.Parameter != null)
            {
                GetUsersModel m = JsonConvert.DeserializeObject<GetUsersModel>((string)e.Parameter);

                IsEditMode = true;
                UserID = m.Id;
                activity.Visibility = Visibility.Visible;
                SubmitButton.Content = "Edytuj użytkownika";

                //LoginModel SavedResponseObj = await helper.ReadFileAsync<LoginModel>("response");

                //RestClient client = new RestClient(IP.Adress);
                //RestRequest request = new RestRequest("Users/get/{id}", Method.GET);
                //request.AddParameter("Authorization", "Bearer " + SavedResponseObj.Token, ParameterType.HttpHeader);
                //request.AddParameter("id", e.Parameter, ParameterType.UrlSegment);
                //IRestResponse response = client.Execute(request);

                

                username.Text = m.UserName;
                password.Password = string.Empty;
                passwordconfirm.Password = string.Empty;
                if (m.Surname != null)
                {
                    name.Text = m.Name;
                    surname.Text = m.Surname;
                }
                email.Text = m.Email;
                activity.IsChecked = m.Active;
                if (m.Role == "User")
                {
                    usertype.SelectedIndex = 0;
                }
                else
                {
                    usertype.SelectedIndex = 1;
                }
            }

            base.OnNavigatedTo(e);
        }

        private async void Add_Click(object senjder, RoutedEventArgs e)
        {
            if (IsEditMode)
            {
                await AddUser("Account/update/{id}", Method.PUT);
            }
            else
            {
                await AddUser("Account/create", Method.POST);
            }
        }

        private async Task AddUser(string resource, Method method)
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
                    Active = activity.IsChecked ?? true,
                    UserType = usertype.SelectedItem.ToString()
                };
                string json = JsonConvert.SerializeObject(userObj);

                RestClient client = new RestClient(IP.Adress);
                RestRequest request = new RestRequest(resource, method);
                request.AddParameter("application/json", json, ParameterType.RequestBody);
                request.AddParameter("Authorization", "Bearer " + SavedResponseObj.Token, ParameterType.HttpHeader);
                if (IsEditMode)
                {
                    request.AddParameter("id", UserID, ParameterType.UrlSegment);
                }

                // execute the request
                IRestResponse response = client.Execute(request);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    SubmitButton.IsEnabled = true;

                    if (IsEditMode)
                    {
                        await new CustomDialog("Użytkownik edytowany poprawnie.", null, CustomDialog.Type.Information).ShowAsync();
                        CleanupInput();

                        Frame rootFrame = Window.Current.Content as Frame;
                        MainPage homePage = rootFrame.Content as MainPage;
                        homePage.NavigationFrame.Navigate(typeof(UsersPage), null, new DrillInNavigationTransitionInfo());
                    }
                    else
                    {
                        await new CustomDialog("Użytkownik stworzony poprawnie.", null, CustomDialog.Type.Information).ShowAsync();
                        CleanupInput();
                    }
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
        private void CleanupInput()
        {
            username.Text = string.Empty;
            password.Password = string.Empty;
            passwordconfirm.Password = string.Empty;
            name.Text = string.Empty;
            surname.Text = string.Empty;
            email.Text = string.Empty;

            IsEditMode = false;
            UserID = 0;
            activity.Visibility = Visibility.Collapsed;
            activity.IsChecked = true;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            MainPage homePage = rootFrame.Content as MainPage;
            homePage.NavigationFrame.Navigate(typeof(UsersPage), null, new DrillInNavigationTransitionInfo());
        }
    }
}