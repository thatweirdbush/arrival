using BookingManagementSystem.ViewModels.Administrator;
using BookingManagementSystem.Core.Models;
using Microsoft.UI.Xaml;
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
                btnAdd.Visibility = Visibility.Collapsed;
                ViewModel.LoadPriorityListDataAsync();
                break;
            case "Elites":
                btnAdd.Visibility = Visibility.Collapsed;
                ViewModel.GetElitePropertyListDataAsync();
                break;
            case "Trendings":
                btnAdd.Visibility = Visibility.Collapsed;
                ViewModel.GetTrendingPropertyListDataAsync();
                break;
            case "Requests":
                ViewModel.GetRequestedPropertyListDataAsync();
                btnAdd.Visibility = Visibility.Visible;
                break;
            default:
                btnAdd.Visibility = Visibility.Collapsed;
                ViewModel.LoadPriorityListDataAsync();
                break;
        }
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

    private void Remove_Click(object sender, RoutedEventArgs e)
    {
        // Get selected filter mode from combobox
        var selectedValueComboBox = ListingItemStatusComboBox.SelectedItem.ToString();

        // Get selected items from the priority list
        var selectedItems = PriorityPropertyListView.SelectedItems;
        var selectedItemsList = selectedItems.ToList();
        
        // Check empty selection
        if (selectedItemsList.Count == 0)
        {
            return;
        }

        // Modify the selected items' properties
        if (selectedValueComboBox == "Requests")
        {
            foreach (var item in selectedItemsList)
            {
                if (item is Property property)
                {
                    property.IsRequested = false;
                }
            }
        }
        else
        {
            foreach (var item in selectedItemsList)
            {
                if (item is Property property)
                {
                    property.IsPriority = false;
                    property.IsFavourite = false;
                }
            }
        }

        // Reload the list
        switch (selectedValueComboBox)
        {
            case "Current":
                ViewModel.LoadPriorityListDataAsync();
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
                ViewModel.LoadPriorityListDataAsync();
                break;
        }

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

    private void AddToPriority_Click(object sender, RoutedEventArgs e)
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
                property.IsRequested = false;
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
            CloseButtonText = "Ok",
            DefaultButton = ContentDialogButton.Close
        }.ShowAsync();
    }

    private void OnCommandBarElementClicked(object sender, RoutedEventArgs e)
    {
        var elementTag = (sender as AppBarButton)?.Tag ?? (sender as MenuFlyoutItem)?.Tag;
        switch (elementTag)
        {
            case "add":
                AddToPriority_Click(sender, e);
                break;
            case "edit":
                Edit_Click(sender, e);
                break;
            case "cancel":
                CancelEditing_Click(sender, e);
                break;
            case "remove":
                Remove_Click(sender, e);
                break;
            case "deselect":
                Deselect_Click(sender, e);
                break;
            case "inverse":
                SelectInverse_Click(sender, e);
                break;
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
