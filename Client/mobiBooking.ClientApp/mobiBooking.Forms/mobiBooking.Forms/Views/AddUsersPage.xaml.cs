using mobiBooking.Core.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace mobiBooking.Forms.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddUsersPage : ContentPage
    {
        private readonly ConnectionModel IP = new ConnectionModel();
        private bool IsEditMode;
        private int UserID;

        string tokenik = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjEiLCJyb2xlIjoiQWRtaW5pc3RyYXRvciIsInVzZXJOYW1lIjoiVGVzdCIsIm5hbWUiOiJNaWNoYcWCIiwic3VyZU5hbWUiOiJUZXN0IiwiZW1haWwiOiJtLndAZy5wbCIsIm5iZiI6MTU1MzUyNjMyMiwiZXhwIjoxNTU0MTMxMTIyLCJpYXQiOjE1NTM1MjYzMjJ9.AzVLluTv6JqW8VhvZuSkOMA0mXB1teeHp7nTE48HkLM";
        public AddUsersPage()
        {
            InitializeComponent();
        }

        private void Add_Click(object senjder, EventArgs e)
        {
            if (IsEditMode)
            {
                AddUser("Account/update/{id}", Method.PUT);
            }
            else
            {
                AddUser("Account/create", Method.POST);
            }
        }

        private void AddUser(string resource, Method method)
        {
            SubmitButton.IsEnabled = false;

            if (!string.IsNullOrEmpty(username.Text) && !string.IsNullOrEmpty(password.Text) && !string.IsNullOrEmpty(passwordconfirm.Text) && password.Text == passwordconfirm.Text && !string.IsNullOrEmpty(email.Text))
            {
                //LoginModel SavedResponseObj = await helper.ReadFileAsync<LoginModel>("response");

                AddUserModel userObj = new AddUserModel
                {
                    UserName = username.Text.Trim(),
                    Password = password.Text.Trim(),
                    Name = name.Text.Trim(),
                    Surname = surname.Text.Trim(),
                    Email = email.Text.Trim(),
                    Active = activity.IsToggled,
                    UserType = usertype.SelectedItem.ToString()
                };
                string json = JsonConvert.SerializeObject(userObj);

                RestClient client = new RestClient(IP.Adress);
                RestRequest request = new RestRequest(resource, method);
                _ = request.AddParameter("application/json", json, ParameterType.RequestBody);
                _ = request.AddParameter("Authorization", "Bearer " + tokenik, ParameterType.HttpHeader);
                if (IsEditMode)
                {
                    _ = request.AddParameter("id", UserID, ParameterType.UrlSegment);
                }
                IRestResponse response = client.Execute(request);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    SubmitButton.IsEnabled = true;

                    if (IsEditMode)
                    {
                        //_ = await new CustomDialog("Użytkownik edytowany poprawnie.", null, CustomDialog.Type.Information).ShowAsync();
                        CleanupInput();

                        //Frame rootFrame = Window.Current.Content as Frame;
                        //MainPage homePage = rootFrame.Content as MainPage;
                        //_ = homePage.NavigationFrame.Navigate(typeof(UsersPage), null, new DrillInNavigationTransitionInfo());
                    }
                    else
                    {
                        //_ = await new CustomDialog("Użytkownik stworzony poprawnie.", null, CustomDialog.Type.Information).ShowAsync();
                        CleanupInput();
                    }
                }
                else
                {
                    //_ = await new CustomDialog("Wystąpił błąd podczas komunikacji z serwerem.", response.StatusCode.ToString(), CustomDialog.Type.Error).ShowAsync();
                    SubmitButton.IsEnabled = true;
                }
            }
            else
            {
                //_ = await new CustomDialog("Wprowadzono błędne dane.", null, CustomDialog.Type.Warning).ShowAsync();
                SubmitButton.IsEnabled = true;
            }
        }

        private void CleanupInput()
        {
            username.Text = string.Empty;
            password.Text = string.Empty;
            passwordconfirm.Text = string.Empty;
            name.Text = string.Empty;
            surname.Text = string.Empty;
            email.Text = string.Empty;

            IsEditMode = false;
            UserID = 0;
            activity.IsVisible = false;
            activity.IsToggled = true;
        }
    }
}