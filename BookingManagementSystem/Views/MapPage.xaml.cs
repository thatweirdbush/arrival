using BookingManagementSystem.ViewModels;

using Microsoft.UI.Xaml.Controls;
using Windows.Devices.Geolocation;

namespace BookingManagementSystem.Views;

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
        BasicGeoposition centerPosition = new BasicGeoposition { Latitude = 10.779850, Longitude = 106.670900 };
        Geopoint centerPoint = new Geopoint(centerPosition);

        MainMap.Center = centerPoint;

        var myLandmarks = new List<MapElement>();
        BasicGeoposition position = new BasicGeoposition { Latitude = 10.779850, Longitude = 106.670900 };
        Geopoint point = new Geopoint(position);

        var icon = new MapIcon
        {
            Location = point,
        };

        myLandmarks.Add(icon);

        var LandmarksLayer = new MapElementsLayer
        {
            MapElements = myLandmarks
        };

        MainMap.Layers.Add(LandmarksLayer);
    }
}
