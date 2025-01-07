using BookingManagementSystem.Contracts.Services;
using BookingManagementSystem.Core.Commons.Enums;
using BookingManagementSystem.Core.Models;
using BookingManagementSystem.ViewModels;
using BookingManagementSystem.ViewModels.Client;
using BookingManagementSystem.ViewModels.Host;
using BookingManagementSystem.Views.Forms;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

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

    private void Edit_Click(object sender, RoutedEventArgs e)
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

    private async void Remove_Click(object sender, RoutedEventArgs e)
    {
        // Get selected items
        var selectedItems = ListingsGridView.SelectedItems.Cast<Property>().ToList();

        // If there aren't any
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
            await ViewModel.RemoveRangeAsync(selectedItems);
        }
    }

    private Task RemoveAll_Click(object sender, RoutedEventArgs e)
    {
        return ViewModel.RemoveAllAsync();
    }

    private void AddNew_Click(object sender, RoutedEventArgs e)
    {
        // Navigate to Create Listing Page
        App.GetService<INavigationService>().NavigateTo(typeof(CreateListingViewModel).FullName!);
    }

    private Task Refresh_Click(object sender, RoutedEventArgs e)
    {
        // Set to default pagination index & loading state
        return ViewModel.RefreshAsync();
    }

    private void SearchListing_Click(object sender, RoutedEventArgs e)
    {
        // Show search box and hide the Seach button
        SearchBoxContent.Visibility = Visibility.Visible;
        btnSearch.Visibility = Visibility.Collapsed;
        SearchBox.Focus(FocusState.Programmatic);
    }

    private async void OnCommandBarElementClicked(object sender, RoutedEventArgs e)
    {
        var element = (sender as AppBarButton)!.Tag;
        switch (element)
        {
            case "add":
                AddNew_Click(sender, e);
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
            case "remove-all":
                await RemoveAll_Click(sender, e);
                break;
            case "search":
                SearchListing_Click(sender, e);
                break;
            case "refresh":
                await Refresh_Click(sender, e);
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

        // Refresh the List
        await ViewModel.RefreshAsync();
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
                ViewModel.ResetPaginationIndex();
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
                        ViewModel.Search(currentQueryToken);
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

    private async void SearchBox_SuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
    {
        // Get the Property and navigate to the Property Detail Page
        if (args.SelectedItem is string query)
        {
            sender.Text = query;
            var propertyId = await ViewModel.GetSingleSearchedItemId(query);
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
                    XamlRoot = XamlRoot
                };
                await dialog.ShowAsync();

                switch (dialog.Result)
                {
                    case EditListingDialog.DialogResult.Edit:
                        Frame.Navigate(typeof(CreateListingPage), property.Id);
                        break;
                    case EditListingDialog.DialogResult.Remove:
                        await ViewModel.RemoveAsync(property);
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
        if (scrollViewer.VerticalOffset >= scrollViewer.ScrollableHeight) // 0px from end of list
        {
            // Check if loading default data or search data
            if (ViewModel.CurrentLoadingState.Equals(LoadingState.Default))
            {
                await ViewModel.LoadNextPageAsync(); // Load default data
            }
            else if (ViewModel.CurrentLoadingState.Equals(LoadingState.Search))
            {
                ViewModel.LoadSearchedData(currentQueryToken); // Load search data
            }
        }
    }
}
