using BookingManagementSystem.ViewModels.Client;

using Microsoft.UI.Xaml.Controls;

namespace BookingManagementSystem.Views.Client;

public sealed partial class MapPage : Page
{
    public MapViewModel ViewModel
    {
        get;
    }

    public MapPage()
    {
        ViewModel = App.GetService<MapViewModel>();
        InitializeComponent();

        // Set up initial map location
        var query = $"10.8,106.72,15z";
        MapWebView2.Source = new Uri($"https://www.google.com/maps/@{query}");
    }
}
