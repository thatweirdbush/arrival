using BookingManagementSystem.Contracts.Services;
using BookingManagementSystem.Core.DTOs;
using BookingManagementSystem.Core.Models;
using BookingManagementSystem.ViewModels;
using BookingManagementSystem.ViewModels.Client;
using BookingManagementSystem.ViewModels.Host;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace BookingManagementSystem.Views.Host;

public sealed partial class ListingPage : Page
{
    public ListingViewModel ViewModel
    {
        get;
    }

    public ListingPage()
    {
        ViewModel = App.GetService<ListingViewModel>();
        InitializeComponent();
    }

    private void btnSelect_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        ListingsGridView.SelectionMode = ListViewSelectionMode.Multiple;
        btnSelect.Visibility = Microsoft.UI.Xaml.Visibility.Collapsed;
        btnCancel.Visibility = Microsoft.UI.Xaml.Visibility.Visible;
        btnDelete.Visibility = Microsoft.UI.Xaml.Visibility.Visible;
    }

    private void btnCancel_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        ListingsGridView.SelectionMode = ListViewSelectionMode.Single;
        btnCancel.Visibility = Microsoft.UI.Xaml.Visibility.Collapsed;
        btnDelete.Visibility = Microsoft.UI.Xaml.Visibility.Collapsed;
        btnSelect.Visibility = Microsoft.UI.Xaml.Visibility.Visible;
    }

    private async void btnDelete_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        // Get selected items and remove them from the list
        var selectedItems = ListingsGridView.SelectedItems.ToList();

        // Check if there are selected items
        if (selectedItems.Count == 0)
        {
            return;
        }

        // Show confirmation dialog
        var confirm = new ContentDialog
        {
            XamlRoot = this.XamlRoot,
            Title = "Remove Listing",
            Content = "Are you sure you want to remove the selected listing(s)?",
            PrimaryButtonText = "Remove",
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
            if (item is Property property)
            {
                ViewModel.DeleteBookingAsync(property);
            }
        }
    }

    private void btnGetStarted_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        // Navigate to Create Listing Page
        App.GetService<INavigationService>().NavigateTo(typeof(CreateListingViewModel).FullName!);
    }

    private void btnAddNewListing_Click(object sender, RoutedEventArgs e)
    {
        // Navigate to Create Listing Page
        App.GetService<INavigationService>().NavigateTo(typeof(CreateListingViewModel).FullName!);
    }
}
