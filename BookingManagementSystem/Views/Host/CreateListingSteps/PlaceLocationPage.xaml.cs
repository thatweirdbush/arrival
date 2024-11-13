using BookingManagementSystem.ViewModels.Host.CreateListingSteps;

using Microsoft.UI.Xaml.Controls;

namespace BookingManagementSystem.Views.Host.CreateListingSteps;

public sealed partial class PlaceLocationPage : Page
{
    public PlaceLocationViewModel ViewModel
    {
        get;
    }

    public PlaceLocationPage()
    {
        ViewModel = App.GetService<PlaceLocationViewModel>();
        InitializeComponent();
    }
}
