using BookingManagementSystem.ViewModels.Administrator;
using BookingManagementSystem.Core.Models;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using static BookingManagementSystem.ViewModels.Administrator.ListingRequestViewModel;

namespace BookingManagementSystem.Views.Administrator;

public sealed partial class ListingRequestPage : Page
{
    public ListingRequestViewModel ViewModel
    {
        get;
    }

    public ListingRequestPage()
    {
        ViewModel = App.GetService<ListingRequestViewModel>();
        InitializeComponent();
    }

    private void Edit_Click(object sender, RoutedEventArgs e)
    {
        PriorityPropertyListView.IsItemClickEnabled = false;
        PriorityPropertyListView.SelectionMode = ListViewSelectionMode.Multiple;
        btnEdit.Visibility = Visibility.Collapsed;
        btnCancel.Visibility = Visibility.Visible;
    }

    private void CancelEditing_Click(object sender, RoutedEventArgs e)
    {
        PriorityPropertyListView.IsItemClickEnabled = true;
        PriorityPropertyListView.SelectionMode = ListViewSelectionMode.Single;
        btnCancel.Visibility = Visibility.Collapsed;
        btnEdit.Visibility = Visibility.Visible;
    }

    private void Deselect_Click(object sender, RoutedEventArgs e)
    {
        PriorityPropertyListView.SelectedItem = null;
    }

    private void SelectInverse_Click(object sender, RoutedEventArgs e)
    {
        if (PriorityPropertyListView.SelectionMode == ListViewSelectionMode.Multiple)
        {
            foreach (var item in PriorityPropertyListView.Items)
            {
                if (!PriorityPropertyListView.SelectedItems.Remove(item)) // Deselect if selected
                {
                    PriorityPropertyListView.SelectedItems.Add(item); // Select if not selected
                }
            }
        }
    }

    private async Task Remove_Click(object sender, RoutedEventArgs e)
    {
        // Get selected items from the priority list
        var selectedItems = PriorityPropertyListView.SelectedItems.Cast<Property>().ToList();

        // Check empty selection
        if (selectedItems.Count == 0) return;

        // Modify the selected items' properties
        if (ViewModel.SelectedFilter.Equals(FilterType.Requests))
        {
            foreach (var item in selectedItems)
            {
                item.IsRequested = false;
                await ViewModel.UpdateAsync(item);
            }
        }
        else
        {
            foreach (var item in selectedItems)
            {
                item.IsPriority = false;
                item.IsFavourite = false;
                await ViewModel.UpdateAsync(item);
            }
        }

        // Save changes to the database
        await ViewModel.SaveChangesAsync();

        // Reload the list
        await ViewModel.ResetPagination();

        // Show the successful dialog
        _ = new ContentDialog
        {
            XamlRoot = XamlRoot,
            Title = "Deleted from list",
            Content = "Items deleted from list successfully!",
            CloseButtonText = "Ok",
            DefaultButton = ContentDialogButton.Close
        }.ShowAsync();
    }

    private async Task AddToPriority_Click(object sender, RoutedEventArgs e)
    {
        // Get selected items from the priority list
        var selectedItems = PriorityPropertyListView.SelectedItems;

        // Check empty selection
        if (selectedItems.Count == 0) return;

        // Modify the selected items' properties
        foreach (var item in selectedItems)
        {
            if (item is Property property)
            {
                property.IsPriority = true;
                property.IsRequested = false;
                await ViewModel.UpdateAsync(property);
            }
        }

        // Save changes to the database
        await ViewModel.SaveChangesAsync();

        // Reload the priority list
        await ViewModel.ResetPagination();

        // Show the successful dialog
        _ = new ContentDialog
        {
            XamlRoot = XamlRoot,
            Title = "Added to list",
            Content = "Items added to priority list successfully!",
            CloseButtonText = "Ok",
            DefaultButton = ContentDialogButton.Close
        }.ShowAsync();
    }

    private Task Refresh_Click(object sender, RoutedEventArgs e)
    {
        // Set to default pagination index & loading state
        return ViewModel.RefreshAsync();
    }

    private async void OnCommandBarElementClicked(object sender, RoutedEventArgs e)
    {
        var elementTag = (sender as AppBarButton)?.Tag ?? (sender as MenuFlyoutItem)?.Tag;
        switch (elementTag)
        {
            case "add":
                await AddToPriority_Click(sender, e);
                break;
            case "edit":
                Edit_Click(sender, e);
                break;
            case "cancel":
                CancelEditing_Click(sender, e);
                break;
            case "remove":
                await Remove_Click(sender, e);
                break;
            case "deselect":
                Deselect_Click(sender, e);
                break;
            case "inverse":
                SelectInverse_Click(sender, e);
                break;
            case "refresh":
                await Refresh_Click(sender, e);
                break;
        }
    }

    private void ListingItemStatusComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var comboBox = (ComboBox)sender;
        var selectedValue = comboBox.SelectedItem.ToString();

        btnAdd.Visibility = selectedValue == "Requests" ? Visibility.Visible : Visibility.Collapsed;
    }

    private void PriorityPropertyListView_ItemClick(object sender, ItemClickEventArgs e)
    {
        if (e.ClickedItem is Property clickedItem)
        {
            ViewModel.OnItemClick(clickedItem);
        }
    }

    private async void ScrollViewer_ViewChanged(ScrollView sender, object args)
    {
        var scrollViewer = sender as ScrollView;
        if (scrollViewer == null) return;

        // Detect when scroll is near the end
        if (scrollViewer.VerticalOffset >= scrollViewer.ScrollableHeight - 10) // 10px from end of list
        {
            await ViewModel.LoadNextPageAsync();
        }
    }
}
