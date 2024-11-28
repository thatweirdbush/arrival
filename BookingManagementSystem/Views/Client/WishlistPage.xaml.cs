using BookingManagementSystem.Core.Models;
using BookingManagementSystem.ViewModels.Client;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace BookingManagementSystem.Views.Client;

public sealed partial class WishlistPage : Page
{
    public WishlistViewModel ViewModel
    {
        get;
    }

    public WishlistPage()
    {
        ViewModel = App.GetService<WishlistViewModel>();
        InitializeComponent();
    }

    private void EditListing_Click(object sender, RoutedEventArgs e)
    {
        WishlistGridView.IsItemClickEnabled = false;
        WishlistGridView.SelectionMode = ListViewSelectionMode.Multiple;
        btnSelect.Visibility = Visibility.Collapsed;
        btnCancel.Visibility = Visibility.Visible;
        btnRemove.Visibility = Visibility.Visible;
    }

    private void CancelEditing_Click(object sender, RoutedEventArgs e)
    {
        WishlistGridView.IsItemClickEnabled = true;
        WishlistGridView.SelectionMode = ListViewSelectionMode.Single;
        btnCancel.Visibility = Visibility.Collapsed;
        btnRemove.Visibility = Visibility.Collapsed;
        btnSelect.Visibility = Visibility.Visible;
    }

    private async void RemoveListing_Click(object sender, RoutedEventArgs e)
    {
        // Get selected items and remove them from the list
        var selectedItems = WishlistGridView.SelectedItems.ToList();

        // Check if there are selected items
        if (selectedItems.Count == 0)
        {
            return;
        }
        // Show confirmation dialog
        var confirm = new ContentDialog
        {
            XamlRoot = XamlRoot,
            Title = "Remove wishlist",
            Content = "Are you sure you want to remove the selected wishlist(s)?",
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
                    ViewModel.RemoveWishlistAsync(property);
                }
            }
        }
    }

    private async void RemoveAllLissting_Click(object sender, RoutedEventArgs e)
    {
        // Show confirmation dialog
        var confirm = new ContentDialog
        {
            XamlRoot = XamlRoot,
            Title = "Remove all wishlists?",
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
            ViewModel.RemoveAllWishlistAsync();
        }
    }

    private void btnGetStarted_Click(object sender, RoutedEventArgs e)
    {
        // Navigate to Home page
        Frame.Navigate(typeof(HomePage));
    }

    private void OnCommandBarElementClicked(object sender, RoutedEventArgs e)
    {
        var element = (sender as AppBarButton)!.Label;
        switch (element)
        {
            case "Select":
                EditListing_Click(sender, e);
                break;
            case "Cancel":
                CancelEditing_Click(sender, e);
                break;
            case "Remove":
                RemoveListing_Click(sender, e);
                break;
            case "Remove all":
                RemoveAllLissting_Click(sender, e);
                break;
        }
    } 
    
    private void btnFavourite_Click(object sender, RoutedEventArgs e)
    {
        // Toggle the favourite button  
        // Change the image source to the filled heart icon  
        if (sender is FrameworkElement frameworkElement
            && frameworkElement.DataContext is Property property)
        {
            property.IsFavourite = !property.IsFavourite;
        }
    }
}
