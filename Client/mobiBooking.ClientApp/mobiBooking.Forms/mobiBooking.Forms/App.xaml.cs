using mobiBooking.Core.Models;
using mobiBooking.Forms.Views;
using Newtonsoft.Json;
using System;
using Xamarin.Forms;

namespace mobiBooking.Forms
{
    public partial class App : Application
    {
        private readonly LoginModel SavedLoginObj = new LoginModel();
        public App()
        {
            InitializeComponent();

            string SavedLoginJson = Current.Properties["SavedResponse"].ToString();
            SavedLoginObj = JsonConvert.DeserializeObject<LoginModel>(SavedLoginJson);

            if (!SavedLoginObj.IsLoged)
            {
                MainPage = new NavigationPage((Page)Activator.CreateInstance(typeof(LoginPage)));
            }
            else
            {
                MainPage = new NavigationPage((Page)Activator.CreateInstance(typeof(MainPage)));
            }
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}