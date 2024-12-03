using BookingManagementSystem.Core.Models;
using BookingManagementSystem.Core.Services;
using BookingManagementSystem.ViewModels.Host.CreateListingSteps;

using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

namespace BookingManagementSystem.Views.Host.CreateListingSteps;

public sealed partial class PlaceLocationPage : Page
{
    public PlaceLocationViewModel? ViewModel
    {
        get; set;
    }

    public PlaceLocationPage()
    {
        InitializeComponent();
        _ = InitializeWebView2Async();
    }

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
        if (e.Parameter is PlaceLocationViewModel viewModel)
        {
            ViewModel = viewModel!;
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
        // Get current URL from WebView2
        var currentURL = sender.Source;
        ParseCoordinatesFromUrl(currentURL);

        // Find nearby place based on current coordinates to automatically fill the location textbox
        var place = await ViewModel!.FindNearbyPlaceAsync();
        if (place != null)
        {
            ViewModel.SelectedCountry = ViewModel.CountryList.FirstOrDefault(c => c.CountryName.Contains(place.CountryName))!;
            ViewModel.SelectedStateOrProvince = place.Name;
            ViewModel.ValidateProcess();
        }
    }

    private void ParseCoordinatesFromUrl(string url)
    {
        // Split string at '@' character
        var splitUrl = url.Split('@');

        if (splitUrl.Length > 1)
        {
            // Get the part containing the coordinates and separate it with a ','
            var coordinates = splitUrl[1].Split('?')[0]; // Cut off the part after the '?'
            var latLong = coordinates.Split(',');

            if (latLong.Length >= 2)
            {
                ViewModel!.CurrentLatitude = double.Parse(latLong[0]);
                ViewModel!.CurrentLongitude = double.Parse(latLong[1]);
            }
        }
    }

    // Event handler for every text box change
    private void PropertyTextBox_TextChanged(object sender, TextChangedEventArgs e)
    {
        ViewModel!.ValidateProcess();
    }
}
