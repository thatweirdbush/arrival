using BookingManagementSystem.Core.Models;
using BookingManagementSystem.ViewModels.Administrator;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace BookingManagementSystem.Views.Administrator;

public sealed partial class ReportPage : Page
{
    public ReportViewModel ViewModel
    {
        get;
    }

    public ReportPage()
    {
        ViewModel = App.GetService<ReportViewModel>();
        InitializeComponent();
    }

    private void Select_Click(object sender, RoutedEventArgs e)
    {
        ReportListView.IsItemClickEnabled = false;
        ReportListView.SelectionMode = ListViewSelectionMode.Multiple;
        btnSelect.Visibility = Visibility.Collapsed;
        btnCancel.Visibility = Visibility.Visible;
        btnRemove.Visibility = Visibility.Visible;
    }

    private void CancelEditing_Click(object sender, RoutedEventArgs e)
    {
        ReportListView.IsItemClickEnabled = true;
        ReportListView.SelectionMode = ListViewSelectionMode.Single;
        btnCancel.Visibility = Visibility.Collapsed;
        btnRemove.Visibility = Visibility.Collapsed;
        btnSelect.Visibility = Visibility.Visible;
    }

    private async Task Remove_Click(object sender, RoutedEventArgs e)
    {
        // Get selected items
        var selectedItems = ReportListView.SelectedItems.Cast<BadReport>().ToList();

        // Check if there aren't any
        if (selectedItems.Count == 0) return;

        // Show confirmation dialog
        var result = await new ContentDialog
        {
            XamlRoot = XamlRoot,
            Title = "Remove selected items?",
            Content = "Once you remove, you can't get them back.",
            PrimaryButtonText = "Remove",
            CloseButtonText = "Cancel",
            DefaultButton = ContentDialogButton.Primary
        }.ShowAsync();

        // If clicked the Remove button
        if (result == ContentDialogResult.Primary)
        {
            foreach (var item in selectedItems)
            {
                await ViewModel.RemoveReportAsync(item);
            }
            await ViewModel.SaveChangesAsync();
        }
    }

    private async Task RemoveAll_Click(object sender, RoutedEventArgs e)
    {
        // Show confirmation dialog
        var result = await new ContentDialog
        {
            XamlRoot = XamlRoot,
            Title = "Remove all items?",
            Content = "Once you remove all, you can't get them back.",
            PrimaryButtonText = "Remove all",
            CloseButtonText = "Cancel",
            DefaultButton = ContentDialogButton.Primary
        }.ShowAsync();

        // If clicked the Remove button
        if (result == ContentDialogResult.Primary)
        {
            await ViewModel.RemoveAllReportsAsync();
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
            case "refresh":
                await Refresh_Click(sender, e);
                break;
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
