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

    private void Edit_Click(object sender, RoutedEventArgs e)
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

    private async void Remove_Click(object sender, RoutedEventArgs e)
    {
        // Get selected items and remove them from the list
        var selectedItems = WishlistGridView.SelectedItems.Cast<Property>().ToList();

        // Check if there are selected items
        if (selectedItems.Count == 0) return;

        // Show confirmation dialog
        var result = await new ContentDialog
        {
            XamlRoot = XamlRoot,
            Title = "Remove wishlist",
            Content = "Are you sure you want to remove the selected wishlist(s)?",
            PrimaryButtonText = "Remove",
            CloseButtonText = "Cancel",
            DefaultButton = ContentDialogButton.Primary
        }.ShowAsync();

        // Check if the user clicked the Remove button
        if (result == ContentDialogResult.Primary)
        {
            // Remove the selected items from the list
            foreach (var item in selectedItems)
            {
                ViewModel.RemoveWishlistAsync(item);
            }
            await ViewModel.SaveChangesAsync();
        }
    }

    private async void RemoveAll_Click(object sender, RoutedEventArgs e)
    {
        // Show confirmation dialog
        var result = await new ContentDialog
        {
            XamlRoot = XamlRoot,
            Title = "Remove all wishlists?",
            Content = "Once you remove all, you can't get them back.",
            PrimaryButtonText = "Remove all",
            CloseButtonText = "Cancel",
            DefaultButton = ContentDialogButton.Primary
        }.ShowAsync();

        // Check if the user clicked the delete button
        if (result == ContentDialogResult.Primary)
        {
            ViewModel.RemoveAllWishlistAsync();
        }
    }

    private Task Refresh_Click(object sender, RoutedEventArgs e)
    {
        return ViewModel.RefreshAsync();
    }

    private async void OnCommandBarElementClicked(object sender, RoutedEventArgs e)
    {
        var element = (sender as AppBarButton)!.Tag;
        switch (element)
        {
            case "select":
                Edit_Click(sender, e);
                break;
            case "cancel":
                CancelEditing_Click(sender, e);
                break;
            case "remove":
                Remove_Click(sender, e);
                break;
            case "remove all":
                RemoveAll_Click(sender, e);
                break;
            case "refresh":
                await Refresh_Click(sender, e);
                break;
        }
    }

    private void btnGetStarted_Click(object sender, RoutedEventArgs e)
    {
        Frame.Navigate(typeof(HomePage));
    }

    private async void btnFavourite_Click(object sender, RoutedEventArgs e)
    {
        // Toggle the favourite button  
        // Change the image source to the filled heart icon  
        if (sender is FrameworkElement frameworkElement
            && frameworkElement.DataContext is Property property)
        {
            property.IsFavourite = !property.IsFavourite;
            await ViewModel.UpdateAsync(property);
            await ViewModel.SaveChangesAsync();
        }
    }

    private async void ScrollViewer_ViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
    {
        var scrollViewer = sender as ScrollViewer;
        if (scrollViewer == null) return;

        // Detect when scroll is near the end
        if (scrollViewer.VerticalOffset >= scrollViewer.ScrollableHeight - 10) // 10px from end of list
        {
            await ViewModel.LoadNextPageAsync();
        }
    }
}
