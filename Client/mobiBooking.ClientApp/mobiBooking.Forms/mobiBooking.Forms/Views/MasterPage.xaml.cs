using mobiBooking.Core.Models;
using Newtonsoft.Json;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace mobiBooking.Forms.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterPage : ContentPage
    {
        private readonly LoginModel SavedLoginObj = new LoginModel();
        public MasterPage()
        {
            InitializeComponent();

            string SavedLoginJson = Application.Current.Properties["SavedResponse"].ToString();
            SavedLoginObj = JsonConvert.DeserializeObject<LoginModel>(SavedLoginJson);

            UserNameText.Text = SavedLoginObj.Name + " " + SavedLoginObj.surName + " - " + SavedLoginObj.role;
        }

        private async void LogOut_Clicked(object sender, EventArgs e)
        {
            LoginModel EmptyLoginObj = new LoginModel()
            {
                IsLoged = false
            };

            Application.Current.Properties["SavedResponse"] = JsonConvert.SerializeObject(EmptyLoginObj);
            await Application.Current.SavePropertiesAsync();

            LoginPage loginPage = new LoginPage();
            await Navigation.PushModalAsync(loginPage);
        }
    }
}