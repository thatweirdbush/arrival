using BookingManagementSystem.Contracts.Services;
using BookingManagementSystem.Core.Models;
using BookingManagementSystem.ViewModels;
using BookingManagementSystem.ViewModels.Client;
using BookingManagementSystem.ViewModels.Host;
using BookingManagementSystem.Views.Forms;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using static BookingManagementSystem.ViewModels.Host.ListingViewModel;

namespace BookingManagementSystem.Views.Host;

public sealed partial class ListingPage : Page
{
    // Properties nessesary for fuzzy searching
    private CancellationTokenSource _debounceTokenSource = new();

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
        ListingsGridView.IsItemClickEnabled = false;
        ListingsGridView.SelectionMode = ListViewSelectionMode.Multiple;
        btnEdit.Visibility = Visibility.Collapsed;
        btnCancel.Visibility = Visibility.Visible;
        btnRemove.Visibility = Visibility.Visible;
    }

    private void CancelEditing_Click(object sender, RoutedEventArgs e)
    {
        ListingsGridView.IsItemClickEnabled = true;
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
                    await ViewModel.RemoveBookingAsync(property);
                }
            }
        }
    }

    private void AddNewListing_Click(object sender, RoutedEventArgs e)
    {
        // Navigate to Create Listing Page
        App.GetService<INavigationService>().NavigateTo(typeof(CreateListingViewModel).FullName!);
    }

    private async void RemoveAllLissting_Click(object sender, RoutedEventArgs e)
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
            await ViewModel.RemoveAllBookingsAsync();
        }
    }

    private void btnGetStarted_Click(object sender, RoutedEventArgs e)
    {
        // Navigate to Create Listing Page
        App.GetService<INavigationService>().NavigateTo(typeof(CreateListingViewModel).FullName!);
    }

    private void SearchListing_Click(object sender, RoutedEventArgs e)
    {
        // Show search box and hide the Seach button
        SearchBoxContent.Visibility = Visibility.Visible;
        btnSearch.Visibility = Visibility.Collapsed;
        SearchBox.Focus(FocusState.Programmatic);
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
                RemoveAllLissting_Click(sender, e);
                break;
            case "Search":
                SearchListing_Click(sender, e);
                break;
        }
    }

    private async void CloseSearchBoxButton_Click(object sender, RoutedEventArgs e)
    {
        // Hide search box and show the Seach button
        SearchBoxContent.Visibility = Visibility.Collapsed;
        btnSearch.Visibility = Visibility.Visible;

        // Clear search box text
        SearchBox.Text = string.Empty;

        // Set to default pagination index & loading state
        ViewModel.ResetPaginationIndex();
        ViewModel.CurrentLoadingState = LoadingState.Default;

        // Reload Property List
        ViewModel.Properties.Clear();
        await ViewModel.LoadPropertyListAsync();
    }

    string currentQueryToken = string.Empty;

    private async void SearchBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
    {
        // Since selecting an item will also change the text,
        // only listen to changes caused by user entering text.
        if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
        {
            // Cancel previous token if any (if user continues typing)
            _debounceTokenSource?.Cancel();
            _debounceTokenSource = new CancellationTokenSource();
            var token = _debounceTokenSource.Token;

            try
            {
                // Wait 400ms to debounce
                await Task.Delay(400, token);

                // Check if token is destroyed before continuing
                token.ThrowIfCancellationRequested();

                var suitableItems = new List<string>();
                var splitText = sender.Text.ToLower().Split(" ");
                ViewModel.Properties.Clear();

                foreach (var line in ViewModel.PropertyNameAndLocationList)
                {
                    var found = splitText.All((key) =>
                    {
                        return line.Contains(key, StringComparison.CurrentCultureIgnoreCase);
                    });
                    if (found)
                    {
                        currentQueryToken = line;
                        suitableItems.Add(currentQueryToken);
                        await ViewModel.SearchPropertiesAsync(currentQueryToken);
                    }
                }
                if (suitableItems.Count == 0)
                {
                    ViewModel.Properties.Clear();
                    suitableItems.Add("No results found");
                }
                sender.ItemsSource = suitableItems;
            }
            catch (OperationCanceledException)
            {
                // Ignore the exception
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }
    }

    private void SearchBox_SuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
    {
        // Get the Property and navigate to the Property Detail Page
        if (args.SelectedItem is string query)
        {
            sender.Text = query;
            var propertyId = ViewModel.GetSingleSearchedProperty(query);
            App.GetService<INavigationService>().NavigateTo(typeof(RentalDetailViewModel).FullName!, propertyId);
        }
    }

    private async void ListingsGridView_ItemClick(object sender, ItemClickEventArgs e)
    {
        if (e.ClickedItem is Property property)
        {
            if (property.Status == PropertyStatus.InProgress)
            {
                // Create an instance of EditListing Dialog
                var dialog = new EditListingDialog(property)
                {
                    // XamlRoot must be set in the case of a ContentDialog running in a Desktop app
                    XamlRoot = XamlRoot
                };
                await dialog.ShowAsync();
                switch (dialog.Result)
                {
                    case EditListingDialog.DialogResult.Edit:
                        Frame.Navigate(typeof(CreateListingPage), property.Id);
                        break;
                    case EditListingDialog.DialogResult.Remove:
                        await ViewModel.RemoveBookingAsync(property);
                        break;
                    case EditListingDialog.DialogResult.None:
                    default:
                        break;
                }
            }
            else
            {
                App.GetService<INavigationService>().SetListDataItemForNextConnectedAnimation(property);
                App.GetService<INavigationService>().NavigateTo(typeof(RentalDetailViewModel).FullName!, property.Id);
            }
        }
    }

    private async void ScrollViewer_ViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
    {
        var scrollViewer = sender as ScrollViewer;
        if (scrollViewer == null) return;

        // Detect when scroll is near the end
        if (scrollViewer.VerticalOffset >= scrollViewer.ScrollableHeight - 10) // 10px from end of list
        {
            // Check if loading default data or search data
            if (ViewModel.CurrentLoadingState.Equals(LoadingState.Default))
            {
                await ViewModel.LoadPropertyListAsync(); // Load default data
            }
            else if (ViewModel.CurrentLoadingState.Equals(LoadingState.Search))
            {
                // Do nothing
            }
        }
    }
}
