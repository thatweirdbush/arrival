using BookingManagementSystem.Contracts.Services;
using BookingManagementSystem.Core.DTOs;
using BookingManagementSystem.ViewModels;
using BookingManagementSystem.ViewModels.Client;

using Microsoft.UI.Xaml;
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

    private void btnSelect_Click(object sender, RoutedEventArgs e)
    {
        TripsGridView.IsItemClickEnabled = false;
        TripsGridView.SelectionMode = ListViewSelectionMode.Multiple;
        btnSelect.Visibility = Visibility.Collapsed;
        btnCancel.Visibility = Visibility.Visible;
        btnDelete.Visibility = Visibility.Visible;
    }

    private void btnCancel_Click(object sender, RoutedEventArgs e)
    {
        TripsGridView.IsItemClickEnabled = true;
        TripsGridView.SelectionMode = ListViewSelectionMode.Single;
        btnCancel.Visibility = Visibility.Collapsed;
        btnDelete.Visibility = Visibility.Collapsed;
        btnSelect.Visibility = Visibility.Visible;
    }

    private async void btnDelete_Click(object sender, RoutedEventArgs e)
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
            CloseButtonText = "Cancel",
            DefaultButton = ContentDialogButton.Primary
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

    private void btnStartSearching_Click(object sender, RoutedEventArgs e)
    {
        // Navigate to Home Page
        App.GetService<INavigationService>().NavigateTo(typeof(HomeViewModel).FullName!);
    }

    private void btnFavourite_Click(object sender, RoutedEventArgs e)
    {
        // Toggle the favourite button  
        // Change the image source to the filled heart icon  
        if (sender is FrameworkElement frameworkElement
            && frameworkElement.DataContext is BookingPropertyViewModel bookingProperty)
        {
            bookingProperty.Property.IsFavourite = !bookingProperty.Property.IsFavourite;
        }
    }
}
