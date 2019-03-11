﻿using Windows.UI.Xaml.Controls;

namespace mobiBooking.UWP.Views
{
    public sealed partial class BookPage : Page
    {
        public BookPage()
        {
            InitializeComponent();
        }

        private void SelectAll_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (SelectAll.IsChecked == true)
            {
                UsersList.SelectAll();
            }
            else
            {
                UsersList.SelectedIndex = -1;
            }
        }
    }
}