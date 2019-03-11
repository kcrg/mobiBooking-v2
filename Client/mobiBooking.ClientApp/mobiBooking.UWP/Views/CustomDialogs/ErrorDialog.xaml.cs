using Windows.UI.Xaml.Controls;

namespace mobiBooking.UWP.Views.CustomDialogs
{
    public sealed partial class ErrorDialog : ContentDialog
    {
        public ErrorDialog()
        {
            InitializeComponent();
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            Hide();
        }
    }
}