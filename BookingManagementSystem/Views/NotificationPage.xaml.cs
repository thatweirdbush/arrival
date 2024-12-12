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
        btnMarkAsReadOrUnread.Visibility = Visibility.Visible;
        btnMarkAllAsRead.Visibility = Visibility.Collapsed;
    }

    private void CancelEditing_Click(object sender, RoutedEventArgs e)
    {
        NotificationListView.IsItemClickEnabled = true;
        NotificationListView.SelectionMode = ListViewSelectionMode.Single;
        btnCancel.Visibility = Visibility.Collapsed;
        btnRemove.Visibility = Visibility.Collapsed;
        btnSelect.Visibility = Visibility.Visible;
        btnMarkAsReadOrUnread.Visibility = Visibility.Collapsed;
        btnMarkAllAsRead.Visibility = Visibility.Visible;
    }

    private async Task Remove_Click(object sender, RoutedEventArgs e)
    {
        // Get selected items and remove them from the list
        var selectedItems = NotificationListView.SelectedItems.Cast<Notification>().ToList();

        // Check if there are selected items
        if (selectedItems.Count == 0) return;

        // Show confirmation dialog
        var result = await new ContentDialog
        {
            XamlRoot = XamlRoot,
            Title = "Remove notifications?",
            Content = "Once you remove, you can't get them back.",
            PrimaryButtonText = "Remove",
            CloseButtonText = "Cancel",
            DefaultButton = ContentDialogButton.Primary
        }.ShowAsync();

        // If the Remove button
        if (result == ContentDialogResult.Primary)
        {
            foreach (var notification in selectedItems)
            {
                await ViewModel.RemoveAsync(notification);
            }
            await ViewModel.SaveChangesAsync();
            ViewModel.CheckListCount();
        }
    }

    private async Task RemoveAll_Click(object sender, RoutedEventArgs e)
    {
        // Show confirmation dialog
        var result = await new ContentDialog
        {
            XamlRoot = XamlRoot,
            Title = "Remove all notifications?",
            Content = "Once you remove all, you can't get them back.",
            PrimaryButtonText = "Remove all",
            CloseButtonText = "Cancel",
            DefaultButton = ContentDialogButton.Primary
        }.ShowAsync();

        // Check if the user clicked the delete button
        if (result == ContentDialogResult.Primary)
        {
            await ViewModel.RemoveAllAsync();
        }
    }

    private async Task MarkAsReadOrUnread_Click(object sender, RoutedEventArgs e)
    {
        // Get selected items and remove them from the list
        var selectedItems = NotificationListView.SelectedItems.Cast<Notification>().ToList();

        // Check if there are selected items
        if (selectedItems.Count == 0) return;

        // Check if all selected items are read
        if (selectedItems.All(n => n.IsRead))
        {
            foreach (var notification in selectedItems)
            {
                await ViewModel.MarkAsUnreadAsync(notification);
            }
        }
        else
        {
            foreach (var notification in selectedItems)
            {
                await ViewModel.MarkAsReadAsync(notification);
            }
        }
        await ViewModel.SaveChangesAsync();
    }

    private async Task MarkAllAsRead_Click(object sender, RoutedEventArgs e)
    {
        await ViewModel.MarkAllAsReadAsync();
    }

    private async void OnCommandBarElementClicked(object sender, RoutedEventArgs e)
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
                await Remove_Click(sender, e);
                break;
            case "remove-all":
                await RemoveAll_Click(sender, e);
                break;
            case "read-unread":
                await MarkAsReadOrUnread_Click(sender, e);
                break;
            case "mark-all-as-read":
                await MarkAllAsRead_Click(sender, e);
                break;
        }
    }

    private void NotificationToggleButton_Click(object sender, RoutedEventArgs e)
    {
        var element = (sender as ToggleButton)!.Tag;
        switch (element)
        {
            case "all":
                {
                    AllNotificationToggleButton.IsChecked = true;
                    UnreadNotificationToggleButton.IsChecked = false;
                    ViewModel.IsSelectedUnreadFilter = false;
                    break;
                }
            case "unread":
                {
                    UnreadNotificationToggleButton.IsChecked = true;
                    AllNotificationToggleButton.IsChecked = false;
                    ViewModel.IsSelectedUnreadFilter = true;
                    break;
                }
        }
    }

    private async void ScrollViewer_ViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
    {
        var scrollViewer = sender as ScrollViewer;
        if (scrollViewer == null) return;

        // Detect when scroll is near the end
        if (scrollViewer.VerticalOffset >= scrollViewer.ScrollableHeight)
        {
            await ViewModel.LoadNextPageAsync();
        }
    }
}
