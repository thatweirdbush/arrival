using BookingManagementSystem.Contracts.Services;
using BookingManagementSystem.Core.Models;
using BookingManagementSystem.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;

namespace BookingManagementSystem.Views;

public sealed partial class NotificationPage : Page
{
    public NotificationViewModel ViewModel
    {
        get;
    }

    public NotificationPage()
    {
        ViewModel = App.GetService<NotificationViewModel>();
        InitializeComponent();
    }

    private void Select_Click(object sender, RoutedEventArgs e)
    {
        NotificationListView.IsItemClickEnabled = false;
        NotificationListView.SelectionMode = ListViewSelectionMode.Multiple;
        btnSelect.Visibility = Visibility.Collapsed;
        btnCancel.Visibility = Visibility.Visible;
        btnRemove.Visibility = Visibility.Visible;
    }

    private void CancelEditing_Click(object sender, RoutedEventArgs e)
    {
        NotificationListView.IsItemClickEnabled = true;
        NotificationListView.SelectionMode = ListViewSelectionMode.Single;
        btnCancel.Visibility = Visibility.Collapsed;
        btnRemove.Visibility = Visibility.Collapsed;
        btnSelect.Visibility = Visibility.Visible;
    }

    private async void Remove_Click(object sender, RoutedEventArgs e)
    {
        // Get selected items and remove them from the list
        var selectedItems = NotificationListView.SelectedItems.ToList();

        // Check if there are selected items
        if (selectedItems.Count == 0)
        {
            return;
        }
        // Show confirmation dialog
        var confirm = new ContentDialog
        {
            XamlRoot = XamlRoot,
            Title = "Remove Notification",
            Content = "Are you sure you want to remove the selected notifications?",
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
                if (item is Notification notification)
                {
                    await ViewModel.RemoveNotificationAsync(notification);
                }
            }
        }
    }

    private async void RemoveAll_Click(object sender, RoutedEventArgs e)
    {
        // Show confirmation dialog
        var confirm = new ContentDialog
        {
            XamlRoot = XamlRoot,
            Title = "Remove all notifications?",
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
            await ViewModel.RemoveAllNotificationAsync();
        }
    }

    private void OnCommandBarElementClicked(object sender, RoutedEventArgs e)
    {
        var element = (sender as AppBarButton)!.Tag;
        switch (element)
        {
            case "select":
                Select_Click(sender, e);
                break;
            case "cancel":
                CancelEditing_Click(sender, e);
                break;
            case "remove":
                Remove_Click(sender, e);
                break;
            case "remove-all":
                RemoveAll_Click(sender, e);
                break;
        }
    }

    private async void NotificationToggleButton_Click(object sender, RoutedEventArgs e)
    {
        var element = (sender as ToggleButton)!.Tag;
        switch (element)
        {
            case "all":
                {   // Uncheck the UnreadNotificationToggleButton
                    UnreadNotificationToggleButton.IsChecked = false;
                    AllNotificationToggleButton.IsChecked = true;

                    // Reload the notification list
                    await ViewModel.LoadNotificationData();
                    break;
                }
            case "unread":
                {   // Uncheck the AllNotificationToggleButton
                    AllNotificationToggleButton.IsChecked = false;
                    UnreadNotificationToggleButton.IsChecked = true;

                    // Reload the notification list
                    await ViewModel.LoadNotificationData(isUnreadFilter: true);
                    break;
                }
        }
    }
}
