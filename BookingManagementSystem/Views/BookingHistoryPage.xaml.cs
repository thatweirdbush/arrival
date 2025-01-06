using BookingManagementSystem.Contracts.Services;
using BookingManagementSystem.Core.Models;
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
        btnRemove.Visibility = Visibility.Visible;
    }

    private void btnCancel_Click(object sender, RoutedEventArgs e)
    {
        TripsGridView.IsItemClickEnabled = true;
        TripsGridView.SelectionMode = ListViewSelectionMode.Single;
        btnCancel.Visibility = Visibility.Collapsed;
        btnRemove.Visibility = Visibility.Collapsed;
        btnSelect.Visibility = Visibility.Visible;
    }

    private async void btnRemove_Click(object sender, RoutedEventArgs e)
    {
        // Get selected items and remove them from the list
        var selectedItems = TripsGridView.SelectedItems.Cast<Booking>().ToList();

        // Check if there are selected items
        if (!selectedItems.Any()) return;

        // Show confirmation dialog
        var result = await new ContentDialog
        {
            XamlRoot = XamlRoot,
            Title = "Remove Booking",
            Content = "Are you sure you want to remove the selected booking(s)?",
            PrimaryButtonText = "Remove",
            CloseButtonText = "Cancel",
            DefaultButton = ContentDialogButton.Primary
        }.ShowAsync();

        // If clicked the Remove button
        if (result == ContentDialogResult.Primary)
        {
            // Remove the selected items from the list
            ViewModel.DeleteBookingRangeAsync(selectedItems);
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
            && frameworkElement.DataContext is Booking booking)
        {
            booking.Property.IsFavourite = !booking.Property.IsFavourite;
        }
    }
}
