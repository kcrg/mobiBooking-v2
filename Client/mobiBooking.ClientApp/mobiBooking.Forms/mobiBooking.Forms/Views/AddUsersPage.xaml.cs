﻿using mobiBooking.Core.Models;
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
        private LoginModel SavedLoginObj = new LoginModel();
        private bool IsEditMode;
        private int UserID;

        public AddUsersPage()
        {
            InitializeComponent();

            string SavedLoginJson = Application.Current.Properties["SavedResponse"].ToString();
            SavedLoginObj = JsonConvert.DeserializeObject<LoginModel>(SavedLoginJson);

            usertype.SelectedIndex = 0;
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

        private async void AddUser(string resource, Method method)
        {
            SubmitButton.IsEnabled = false;

            if (!string.IsNullOrEmpty(username.Text) && !string.IsNullOrEmpty(password.Text) && !string.IsNullOrEmpty(passwordconfirm.Text) && password.Text == passwordconfirm.Text && !string.IsNullOrEmpty(email.Text))
            {
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
                _ = request.AddParameter("Authorization", "Bearer " + SavedLoginObj.Token, ParameterType.HttpHeader);
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
                        _ = await DisplayAlert("Informacja", "Użytkownik edytowany poprawnie.", null, "Ok");
                        CleanupInput();

                        //Frame rootFrame = Window.Current.Content as Frame;
                        //MainPage homePage = rootFrame.Content as MainPage;
                        //_ = homePage.NavigationFrame.Navigate(typeof(UsersPage), null, new DrillInNavigationTransitionInfo());
                    }
                    else
                    {
                        _ = await DisplayAlert("Informacja", "Użytkownik stworzony poprawnie.", null, "Ok");
                        CleanupInput();
                    }
                }
                else
                {
                    _ = await DisplayAlert("Błąd", "Wystąpił błąd podczas komunikacji z serwerem.", null, "Ok");
                    SubmitButton.IsEnabled = true;
                }
            }
            else
            {
                _ = await DisplayAlert("Ostrzeżenie", "Wprowadzono błędne dane.", null, "Ok");
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