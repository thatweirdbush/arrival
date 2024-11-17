using BookingManagementSystem.Contracts.Services;
using BookingManagementSystem.Core.Models;
using BookingManagementSystem.ViewModels;
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

    private void EditListing_Click(object sender, RoutedEventArgs e)
    {
        ListingsGridView.SelectionMode = ListViewSelectionMode.Multiple;
        btnEdit.Visibility = Visibility.Collapsed;
        btnCancel.Visibility = Visibility.Visible;
        btnRemove.Visibility = Visibility.Visible;
    }

    private void CancelEditing_Click(object sender, RoutedEventArgs e)
    {
        ListingsGridView.SelectionMode = ListViewSelectionMode.Single;
        btnCancel.Visibility = Visibility.Collapsed;
        btnRemove.Visibility = Visibility.Collapsed;
        btnEdit.Visibility = Visibility.Visible;
    }

    private async void RemoveListing_Click(object sender, RoutedEventArgs e)
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
            XamlRoot = XamlRoot,
            Title = "Remove Listing",
            Content = "Are you sure you want to remove the selected listing(s)?",
            PrimaryButtonText = "Remove",
            CloseButtonText = "Cancel",
            DefaultButton = ContentDialogButton.Primary
        };
        var result = await confirm.ShowAsync();

        // Check if the user clicked the delete button
        if (result == ContentDialogResult.Primary)
        {
            // Remove the selected items from the list
            foreach (var item in selectedItems)
            {
                if (item is Property property)
                {
                    ViewModel.RemoveBookingAsync(property);
                }
            }
        }
    }

    private void AddNewListing_Click(object sender, RoutedEventArgs e)
    {
        // Navigate to Create Listing Page
        App.GetService<INavigationService>().NavigateTo(typeof(CreateListingViewModel).FullName!);
    }

    private async void RemoveAllPhotos_Click(object sender, RoutedEventArgs e)
    {
        // Show confirmation dialog
        var confirm = new ContentDialog
        {
            XamlRoot = XamlRoot,
            Title = "Remove all listings?",
            Content = "Once you remove all, you can't get them back.",
            PrimaryButtonText = "Remove all",
            CloseButtonText = "Cancel",
            DefaultButton = ContentDialogButton.Primary
        };

        var result = await confirm.ShowAsync();

        // Check if the user clicked the delete button
        if (result == ContentDialogResult.Primary)
        {
            // Remove all listings
            ViewModel.RemoveAllBookingsAsync();
        }
    }

    private void btnGetStarted_Click(object sender, RoutedEventArgs e)
    {
        // Navigate to Create Listing Page
        App.GetService<INavigationService>().NavigateTo(typeof(CreateListingViewModel).FullName!);
    }

    private void OnCommandBarElementClicked(object sender, RoutedEventArgs e)
    {
        var element = (sender as AppBarButton)!.Label;
        switch (element)
        {
            case "Add":
                AddNewListing_Click(sender, e);
                break;
            case "Edit":
                EditListing_Click(sender, e);
                break;
            case "Cancel":
                CancelEditing_Click(sender, e);
                break;
            case "Remove":
                RemoveListing_Click(sender, e);
                break;
            case "Remove all":
                RemoveAllPhotos_Click(sender, e);
                break;
        }
    }
}
