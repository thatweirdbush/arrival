using BookingManagementSystem.ViewModels.Administrator;
using BookingManagementSystem.Core.Models;

using Microsoft.UI.Xaml.Controls;

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

    private void ListingItemStatusComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var comboBox = (ComboBox)sender;
        var selectedValue = comboBox.SelectedItem.ToString();

        switch (selectedValue)
        {
            case "Current":
                EditOptionMenuFlyoutItem_AddToPriority.Visibility = Microsoft.UI.Xaml.Visibility.Collapsed;
                ViewModel.GetPriorityPropertyListDataAsync();
                break;
            case "Elites":
                EditOptionMenuFlyoutItem_AddToPriority.Visibility = Microsoft.UI.Xaml.Visibility.Collapsed;
                ViewModel.GetElitePropertyListDataAsync();
                break;
            case "Trendings":
                EditOptionMenuFlyoutItem_AddToPriority.Visibility = Microsoft.UI.Xaml.Visibility.Collapsed;
                ViewModel.GetTrendingPropertyListDataAsync();
                break;
            case "Requests":
                ViewModel.GetRequestedPropertyListDataAsync();
                EditOptionMenuFlyoutItem_AddToPriority.Visibility = Microsoft.UI.Xaml.Visibility.Visible;
                break;
            default:
                EditOptionMenuFlyoutItem_AddToPriority.Visibility = Microsoft.UI.Xaml.Visibility.Collapsed;
                ViewModel.GetPriorityPropertyListDataAsync();
                break;
        }
    }

    private void btnEditList_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        PriorityPropertyListView.SelectionMode = ListViewSelectionMode.Multiple;
        btnEditList.Visibility = Microsoft.UI.Xaml.Visibility.Collapsed;
        btnCancelEditingList.Visibility = Microsoft.UI.Xaml.Visibility.Visible;
        btnEditOptions.Visibility = Microsoft.UI.Xaml.Visibility.Visible;
    }

    private void btnCancelEditingList_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        PriorityPropertyListView.SelectionMode = ListViewSelectionMode.Single;
        btnCancelEditingList.Visibility = Microsoft.UI.Xaml.Visibility.Collapsed;
        btnEditOptions.Visibility = Microsoft.UI.Xaml.Visibility.Collapsed;
        btnEditList.Visibility = Microsoft.UI.Xaml.Visibility.Visible;
    }

    private void btnEditOptions_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        var clickedItem = sender as MenuFlyoutItem;

        if (clickedItem?.Tag?.ToString() == "priority")
        {
            // Get selected items from the priority list
            var selectedItems = PriorityPropertyListView.SelectedItems;
            var selectedItemsList = selectedItems.ToList();

            // Check empty selection
            if (selectedItemsList.Count == 0)
            {
                return;
            }

            // Modify the selected items' properties
            foreach (var item in selectedItemsList)
            {
                if (item is Property property)
                {
                    property.IsPriority = true;
                    property.IsFavourite = false;
                }
            }
            // Reload the priority list
            ViewModel.GetRequestedPropertyListDataAsync();

            // Show the successful dialog
            _ = new ContentDialog
            {
                XamlRoot = XamlRoot,
                Title = "Added to list",
                Content = "Items added to priority list successfully!",
                CloseButtonText = "Ok"
            }.ShowAsync();
        }
        else if (clickedItem?.Tag?.ToString() == "delete")
        {
            // Get selected items from the priority list
            var selectedItems = PriorityPropertyListView.SelectedItems;
            var selectedItemsList = selectedItems.ToList();

            // Check empty selection
            if (selectedItemsList.Count == 0)
            {
                return;
            }

            // Modify the selected items' properties
            foreach (var item in selectedItemsList)
            {
                if (item is Property property)
                {
                    property.IsPriority = false;
                    property.IsFavourite = false;
                }
            }
            // Get selected filter mode from combobox
            var selectedValueComboBox = ListingItemStatusComboBox.SelectedItem.ToString();

            // Reload the list
            switch (selectedValueComboBox)
            {
                case "Current":
                    ViewModel.GetPriorityPropertyListDataAsync();
                    break;
                case "Elites":
                    ViewModel.GetElitePropertyListDataAsync();
                    break;
                case "Trendings":
                    ViewModel.GetTrendingPropertyListDataAsync();
                    break;
                case "Requests":
                    ViewModel.GetRequestedPropertyListDataAsync();
                    break;
                default:
                    ViewModel.GetPriorityPropertyListDataAsync();
                    break;
            }

            // Show the successful dialog
            _ = new ContentDialog
            {
                XamlRoot = XamlRoot,
                Title = "Deleted from list",
                Content = "Items deleted from list successfully!",
                CloseButtonText = "Ok"
            }.ShowAsync();
        }
        else if (clickedItem?.Tag?.ToString() == "deselect")
        {
            PriorityPropertyListView.SelectedItems.Clear();
        }
        else if (clickedItem?.Tag?.ToString() == "invert")
        {
            foreach (var item in PriorityPropertyListView.Items)
            {
                if (PriorityPropertyListView.SelectedItems.Contains(item))
                {
                    PriorityPropertyListView.SelectedItems.Remove(item); // Deselect if selected
                }
                else
                {
                    PriorityPropertyListView.SelectedItems.Add(item); // Select if not selected
                }
            }
        }
    }

    private void PriorityPropertyListView_ItemClick(object sender, ItemClickEventArgs e)
    {
        if (e.ClickedItem is Property clickedItem)
        {
            ViewModel.OnItemClick(clickedItem);
        }
    }
}
