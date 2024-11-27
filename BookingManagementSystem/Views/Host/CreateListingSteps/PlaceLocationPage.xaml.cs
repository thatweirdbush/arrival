using BookingManagementSystem.Core.Models;
using BookingManagementSystem.Core.Services;
using BookingManagementSystem.ViewModels.Host.CreateListingSteps;

using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

namespace BookingManagementSystem.Views.Host.CreateListingSteps;

public sealed partial class PlaceLocationPage : Page
{
    // Properties nessesary for Geographic Names searching
    private CancellationTokenSource _debounceTokenSource = new();
    private readonly GeographicNameService _geographicNamesService;
    public PlaceLocationViewModel? ViewModel
    {
        get; set;
    }

    public PlaceLocationPage()
    {
        InitializeComponent();
        _ = InitializeWebView2Async();
        _geographicNamesService = App.GetService<GeographicNameService>();
    }

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
        if (e.Parameter is PlaceLocationViewModel viewModel)
        {
            ViewModel = viewModel;
        }
        base.OnNavigatedTo(e);
    }

    private async Task InitializeWebView2Async()
    {
        // Ensure WebView2 is initialized
        await MapWebView2.EnsureCoreWebView2Async();

        // Register for the SourceChanged event after CoreWebView2 has been initialized
        MapWebView2.CoreWebView2.SourceChanged += CoreWebView2_SourceChanged;
    }

    private async void CoreWebView2_SourceChanged(Microsoft.Web.WebView2.Core.CoreWebView2 sender, object args)
    {
        // Only process if the query is from AutoSuggestBox
        // Because when user clicks on a suggestion from AutoSuggestBox, all works are already done
        if (isLastQueryFromAutoSuggestBox)
        {
            debounceWebViewSourceChange--;
            return;
        }
        // Get current URL from WebView2
        var currentURL = sender.Source;
        ParseCoordinatesFromUrl(currentURL);

        if (ViewModel != null)
        {
            // Find nearby place based on current coordinates to automatically fill the location textbox
            ViewModel.CurrentLocation = await _geographicNamesService.FindNearbyPlaceAsync(ViewModel.CurrentLatitude, ViewModel.CurrentLongitude);
            ViewModel.ValidateProcess();
        }
    }

    private void ParseCoordinatesFromUrl(string url)
    {
        if (ViewModel == null)
        {
            return;
        }
        // Split string at '@' character
        var splitUrl = url.Split('@');

        if (splitUrl.Length > 1)
        {
            // Get the part containing the coordinates and separate it with a ','
            var coordinates = splitUrl[1].Split('?')[0]; // Cut off the part after the '?'
            var latLong = coordinates.Split(',');

            if (latLong.Length >= 2)
            {
                ViewModel.CurrentLatitude = double.Parse(latLong[0]);
                ViewModel.CurrentLongitude = double.Parse(latLong[1]);
            }
        }
    }

    // Flag to debounce the WebView2 SourceChanged event & to check if the query is from AutoSuggestBox
    private int debounceWebViewSourceChange = 0;
    private bool isLastQueryFromAutoSuggestBox => debounceWebViewSourceChange > 0;

    private async void LocationTextBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
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
                var suggestions = await _geographicNamesService.SearchLocationsAsync(query);

                // Display list of suggestions
                sender.ItemsSource = suggestions;
            }
            catch (Exception)
            {
            }
            finally
            {
                ViewModel?.ValidateProcess();
            }
        }
    }

    private async void LocationTextBox_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
    {
        if (args.ChosenSuggestion != null)
        {
            var query = sender.Text;
            var nearbyLocations = await _geographicNamesService.SearchWikipediaAsync(query, 1);

            // Update coordinates based on the chosen location
            var location = nearbyLocations.FirstOrDefault();
            if (location != null)
            {
                ViewModel!.CurrentLatitude = (double)location.Latitude;
                ViewModel.CurrentLongitude = (double)location.Longitude;

                // Each time we navigate to the below Url, the WebView2 Sourch will redirect one more time
                // Don't believe? Ask Google
                debounceWebViewSourceChange = 2;

                // Update MapWebView2 to show the chosen location
                MapWebView2.Source = new Uri($"https://www.google.com/maps/@{ViewModel.CurrentLatitude},{ViewModel.CurrentLongitude},15z");
            }
        }
        ViewModel?.ValidateProcess();
    }
}
