using BookingManagementSystem.ViewModels.Host.CreateListingSteps;

using Microsoft.UI.Xaml.Controls;

namespace BookingManagementSystem.Views.Host.CreateListingSteps;

public sealed partial class PlacePhotosPage : Page
{
    public PlacePhotosViewModel ViewModel
    {
        get;
    }

    public PlacePhotosPage()
    {
        ViewModel = App.GetService<PlacePhotosViewModel>();
        InitializeComponent();
    }
}
