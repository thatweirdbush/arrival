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

    public HomeViewModel ViewModel { get; }

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

    private void btnCheckInCalendarWrapper_Click(object sender, RoutedEventArgs e)
    {
        // Show the calendar flyout
        CheckInCalendar.IsCalendarOpen = true;
    }

    private void btnCheckOutCalendarWrapper_Click(object sender, RoutedEventArgs e)
    {
        // Show the calendar flyout
        CheckOutCalendar.IsCalendarOpen = true;
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
