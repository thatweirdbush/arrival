using BookingManagementSystem.Core.DTOs;
using BookingManagementSystem.ViewModels;

using Microsoft.UI.Xaml.Controls;

namespace BookingManagementSystem.Views;

public sealed partial class BookingHistoryPage : Page
{
    public BookingHistoryViewModel ViewModel
    {
        get;
    }

    public BookingHistoryPage()
    {
        ViewModel = App.GetService<BookingHistoryViewModel>();
        InitializeComponent();
    }

    private void btnSelect_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        TripsGridView.SelectionMode = ListViewSelectionMode.Multiple;
        btnSelect.Visibility = Microsoft.UI.Xaml.Visibility.Collapsed;
        btnCancel.Visibility = Microsoft.UI.Xaml.Visibility.Visible;
        btnDelete.Visibility = Microsoft.UI.Xaml.Visibility.Visible;
    }

    private void btnCancel_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        TripsGridView.SelectionMode = ListViewSelectionMode.Single;
        btnCancel.Visibility = Microsoft.UI.Xaml.Visibility.Collapsed;
        btnDelete.Visibility = Microsoft.UI.Xaml.Visibility.Collapsed;
        btnSelect.Visibility = Microsoft.UI.Xaml.Visibility.Visible;
    }

    private async void btnDelete_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        // Get selected items and remove them from the list
        var selectedItems = TripsGridView.SelectedItems.ToList();

        // Check if there are selected items
        if (selectedItems.Count == 0)
        {
            return;
        }

        // Show confirmation dialog
        var confirm = new ContentDialog
        {
            XamlRoot = this.XamlRoot,
            Title = "Delete Booking",
            Content = "Are you sure you want to cancel the selected booking(s)?",
            PrimaryButtonText = "Delete",
            CloseButtonText = "Cancel"
        };

        var result = await confirm.ShowAsync();

        // Check if the user clicked the delete button
        if (result != ContentDialogResult.Primary)
        {
            return;
        }

        // Remove the selected items from the list
        foreach (var item in selectedItems)
        {
            if (item is BookingPropertyViewModel bookingPropertyViewModel)
            {
                ViewModel.DeleteBookingAsync(bookingPropertyViewModel);
            }
        }
    }
}
