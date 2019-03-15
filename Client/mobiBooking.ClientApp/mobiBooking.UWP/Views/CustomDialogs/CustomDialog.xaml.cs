using Windows.UI.Xaml.Controls;

namespace mobiBooking.UWP.Views.CustomDialogs
{
    public sealed partial class CustomDialog : ContentDialog
    {
        public enum Type
        {
            Error,
            Information,
            Warning
        }

        public CustomDialog(string message, string errorcode, Type type = Type.Information)
        {
            InitializeComponent();

            MessageText.Text = message;
            if (errorcode != null)
            {
                ErrorCodeText.Text = "Kod błędu: " + errorcode;
            }
            
            switch (type)
            {
                case Type.Error:
                    Title = "Błąd";
                    break;
                case Type.Warning:
                    Title = "Ostrzeżenie";
                    break;
                case Type.Information:
                    Title = "Informacja";
                    break;
            }
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            Hide();
        }
    }
}