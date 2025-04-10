﻿using BookingManagementSystem.Core.Models;
using BookingManagementSystem.ViewModels.Account;
using BookingManagementSystem.ViewModels.Client;

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace BookingManagementSystem.Views.Client;
public sealed partial class HomePage : Page
{
    // Properties nessesary for Geographic Names searching
    private CancellationTokenSource _debounceTokenSource = new();

    public HomeViewModel ViewModel
    {
        get;
    }

    public HomePage()
    {
        InitializeComponent();
        ViewModel = App.GetService<HomeViewModel>();
    }

    private void btnToggleSwitchWrapper_Click(object sender, RoutedEventArgs e)
    {
        // Toggle the switch
        ToggleSwitchDisplayTax.IsOn = !ToggleSwitchDisplayTax.IsOn;
    }

    private void HiddenCalendarView_Click(object sender, RoutedEventArgs e)
    {
        // Open the HiddenCalendarViewFlyout
        HiddenCalendarView.Flyout.ShowAt(HiddenCalendarView);
    }

    private void btnSearchDestinationWrapper_Click(object sender, RoutedEventArgs e)
    {
        // Focus on AutoSuggestBox
        DestinationAutoSuggestBox.Focus(FocusState.Programmatic);
    }

    private async void btnFavourite_Click(object sender, RoutedEventArgs e)
    {
        // Check if the user is logged in
        if (LoginViewModel.CurrentUser == null)
        {
            await ShowContentDialogAsync("Login required", "Please login to submit this question!");
            return;
        }

        // Toggle the favourite button  
        // That change the image source to the filled heart icon  
        if (sender is FrameworkElement frameworkElement
            && frameworkElement.DataContext is Property property)
        {
            await ViewModel.ToggleFavoriteAsync(property);
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
        // Update the UI first to avoid misleading UTC time vs local time
        btnCheckInCalendar.Content = checkInDate.ToString("MMMM d");
        btnCheckOutCalendar.Content = checkOutDate.ToString("MMMM d");

        // Update the schedule based on the selected dates
        ViewModel.CheckInDate = checkInDate.UtcDateTime;
        ViewModel.CheckOutDate = checkOutDate.UtcDateTime;

        // Handle ambiguous bug when remove the min date
        if (ViewModel.CheckInDate > ViewModel.CheckOutDate)
        {
            ViewModel.CheckInDate = ViewModel.CheckOutDate;
        }
    }

    private async void DestinationAutoSuggestBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
    {
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

                // After 400ms, call search API
                var query = sender.Text;
                var suggestions = await ViewModel.SearchLocationsToStringAsync(query);

                // Display list of suggestions
                sender.ItemsSource = suggestions;
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

    private async void btnFilterDestination_Click(object sender, RoutedEventArgs e)
    {
        // Filter Properties based on Destination Type - Preset Filter
        if (sender is FrameworkElement frameworkElement
            && frameworkElement.DataContext is DestinationTypeSymbol destinationTypeSymbol)
        {
            ViewModel.SelectedPresetFilter = destinationTypeSymbol.DestinationType;
            await ViewModel.RefreshAsync();
        }
    }

    private void ToggleSwitchDisplayTax_Toggled(object sender, RoutedEventArgs e)
    {
        ViewModel.ToggleDisplayPropertiesPriceWithTax(ToggleSwitchDisplayTax.IsOn);
    }

    private async void ScrollViewer_ViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
    {
        var scrollViewer = sender as ScrollViewer;
        if (scrollViewer == null) return;

        // Detect when scroll is near the end
        if (scrollViewer.VerticalOffset >= scrollViewer.ScrollableHeight) // 0px from end of list
        {
            await ViewModel.LoadPropertiesAsync();
        }
    }

    private async Task ShowContentDialogAsync(string title, string content)
    {
        var dialog = new ContentDialog
        {
            XamlRoot = XamlRoot,
            Title = title,
            Content = content,
            CloseButtonText = "Ok",
            DefaultButton = ContentDialogButton.Close
        };

        await dialog.ShowAsync();
    }

    private void CalendarView_CalendarViewDayItemChanging(CalendarView sender, CalendarViewDayItemChangingEventArgs args)
    {
        // Must explicitly handle state for both branches (past and future)
        // As the CalendarView reuses items, leading to unwanted state if not handled properly
        if (args.Item.Date < DateTimeOffset.Now.Date)
        {
            // Disable past dates
            args.Item.IsEnabled = false;
        }
        else
        {
            // Enable future dates
            args.Item.IsEnabled = true;
        }
    }
}
