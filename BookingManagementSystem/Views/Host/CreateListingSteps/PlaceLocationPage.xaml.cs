using BookingManagementSystem.Core.Models;
using BookingManagementSystem.ViewModels.Host.CreateListingSteps;

using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

namespace BookingManagementSystem.Views.Host.CreateListingSteps;

public sealed partial class PlaceLocationPage : Page
{
    private string _currentURL = string.Empty;
    private double _currentLatitude = 0.0;
    private double _currentLongitude = 0.0;
    public PlaceLocationViewModel ViewModel
    {
        get;
    }

    public PlaceLocationPage()
    {
        ViewModel = App.GetService<PlaceLocationViewModel>();
        InitializeComponent();
        _ = InitializeWebView2Async();
    }

    private async Task InitializeWebView2Async()
    {
        // Ensure WebView2 is initialized
        await MapWebView2.EnsureCoreWebView2Async();

        // Register for the SourceChanged event after CoreWebView2 has been initialized
        MapWebView2.CoreWebView2.SourceChanged += CoreWebView2_SourceChanged;
    }

    private void CoreWebView2_SourceChanged(Microsoft.Web.WebView2.Core.CoreWebView2 sender, object args)
    {
        // Get current URL from WebView2
        _currentURL = sender.Source;
        ParseCoordinatesFromUrl(ref _currentLatitude, ref _currentLongitude);

        // Save to ViewModel
        ViewModel.SetPropertyCoordinates(_currentLatitude, _currentLongitude);
    }

    private void ParseCoordinatesFromUrl(ref double CurrentLatitude, ref double CurrentLongitude)
    {
        // Split string at '@' character
        var splitUrl = _currentURL.Split('@');

        if (splitUrl.Length > 1)
        {
            // Get the part containing the coordinates and separate it with a ','
            var coordinates = splitUrl[1].Split('?')[0]; // Cut off the part after the '?'
            var latLong = coordinates.Split(',');

            if (latLong.Length >= 2)
            {
                CurrentLatitude = double.Parse(latLong[0]);
                CurrentLongitude = double.Parse(latLong[1]);
            }
        }
    }
}
