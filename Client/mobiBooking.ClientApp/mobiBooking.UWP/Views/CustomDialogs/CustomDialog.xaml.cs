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

        public CustomDialog(string message, Type type = Type.Information)
        {
            InitializeComponent();

            MessageText.Text = message;

            switch (type)
            {
                case Type.Error:
                    Title = "Błąd";
                    break;
                case Type.Information:
                    Title = "Informacja";
                    break;
                case Type.Warning:
                    Title = "Ostrzeżenie";
                    break;
            }
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            Hide();
        }
    }
}