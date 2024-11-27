using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using BookingManagementSystem.ViewModels.Client;
using BookingManagementSystem.Core.Models;
using BookingManagementSystem.Core.Services;

namespace BookingManagementSystem.Views.Client;
public sealed partial class HomePage : Page
{
    // Properties nessesary for Geographic Names searching
    private CancellationTokenSource _debounceTokenSource = new();
    private readonly GeographicNameService _geographicNamesService = new();
    private const string GeoNamesUsername = "thatweirdbush";

    public HomeViewModel ViewModel
    {
        get;
    }

    public HomePage()
    {
        ViewModel = App.GetService<HomeViewModel>();
        InitializeComponent();
    }

    private void btnToggleSwitchWrapper_Click(object sender, RoutedEventArgs e)
    {
        // Toggle the switch
        ToggleSwitchDisplayTax.IsOn = !ToggleSwitchDisplayTax.IsOn;
    }

    private void HiddenMultiDatePicker_Click(object sender, RoutedEventArgs e)
    {
        // Open the HiddenMultiDatePicker Flyout
        HiddenMultiDatePicker.Flyout.ShowAt(HiddenMultiDatePicker);
    }

    private void btnSearchDestinationWrapper_Click(object sender, RoutedEventArgs e)
    {
        // Focus on AutoSuggestBox
        DestinationAutoSuggestBox.Focus(FocusState.Programmatic);
    }

    private void btnFavourite_Click(object sender, RoutedEventArgs e)
    {
        // Toggle the favourite button  
        // That change the image source to the filled heart icon  
        if (sender is FrameworkElement frameworkElement
            && frameworkElement.DataContext is Property property)
        {
            property.IsFavourite = !property.IsFavourite;
        }
    }

    private bool _isUpdatingDates; // Flag to prevent logic processing when updating dates

    private void MultiDatePicker_SelectedDatesChanged(CalendarView sender, CalendarViewSelectedDatesChangedEventArgs args)
    {
        // Prevent logic processing when updating
        if (_isUpdatingDates) return;

        try
        {
            _isUpdatingDates = true;

            // Get the list of currently selected days
            var selectedDates = sender.SelectedDates.Select(d => d.Date).ToList();

            // If no date is selected, do nothing
            if (!selectedDates.Any()) return;

            // Handle adding days logic
            if (args.AddedDates.Any())
            {
                // Newly selected date
                var newlySelectedDate = args.AddedDates[0].Date;

                // Update the selected date range based on the current list
                UpdateSelectedDateRange(sender, selectedDates.Min(), newlySelectedDate);
                UpdateSchedule(selectedDates.Min(), newlySelectedDate);
            }
            // Handle removing days logic
            else if (args.RemovedDates.Any())
            {
                // Removed date
                var removedDate = args.RemovedDates[0].Date;

                // Update the selected date range based on the current list
                UpdateSelectedDateRange(sender, selectedDates.Min(), removedDate);
                UpdateSchedule(selectedDates.Min(), removedDate);
            }
        }
        finally
        {
            _isUpdatingDates = false;
        }
    }

    // Update the selected date range in the CalendarView
    private void UpdateSelectedDateRange(CalendarView sender, DateTimeOffset startDate, DateTimeOffset endDate)
    {
        // Clear all selected dates
        sender.SelectedDates.Clear();

        // Add the selected date range to the CalendarView
        for (var date = startDate; date <= endDate; date = date.AddDays(1))
        {
            sender.SelectedDates.Add(date);
        }
    }

    // Update the schedule based on the selected dates to ViewModel and UI
    private void UpdateSchedule(DateTimeOffset checkInDate, DateTimeOffset checkOutDate)
    {
        // Update the schedule based on the selected dates
        ViewModel.CheckInDate = checkInDate;
        ViewModel.CheckOutDate = checkOutDate;

        // Handle ambiguous bug when remove the min date
        if (ViewModel.CheckInDate > ViewModel.CheckOutDate)
        {
            ViewModel.CheckInDate = ViewModel.CheckOutDate;
        }
        // Update the UI
        btnCheckInCalendar.Content = ViewModel.CheckInDate.ToString("MMMM d");
        btnCheckOutCalendar.Content = ViewModel.CheckOutDate.ToString("MMMM d");
    }

    private async void DestinationAutoSuggestBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
    {
        if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
        {
            // Cancel previous token if any (if user continues typing)
            _debounceTokenSource?.Cancel();
            _debounceTokenSource = new CancellationTokenSource();

            try
            {
                // Wait 300ms to debounce
                await Task.Delay(300, _debounceTokenSource.Token);

                // After 300ms, call search API
                var query = sender.Text;
                var suggestions = await _geographicNamesService.SearchLocationsAsync(query, GeoNamesUsername);

                // Display list of suggestions
                sender.ItemsSource = suggestions;
            }
            catch (Exception)
            {
            }
        }
    }

    private void btnFilterDestination_Click(object sender, RoutedEventArgs e)
    {
        // Filter Properties based on DestinationType  
        if (sender is FrameworkElement frameworkElement
            && frameworkElement.DataContext is DestinationTypeSymbol destinationTypeSymbol)
        {
            ViewModel.FilterProperties(destinationTypeSymbol);
        }
    }
}
