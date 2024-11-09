using BookingManagementSystem.ViewModels.Host.CreateListingSteps;

using Microsoft.UI.Xaml.Controls;

namespace BookingManagementSystem.Views.Host.CreateListingSteps;

public sealed partial class AmenitiesPage : Page
{
    public AmenitiesViewModel ViewModel
    {
        get;
    }

    public AmenitiesPage()
    {
        ViewModel = App.GetService<AmenitiesViewModel>();
        InitializeComponent();
    }
}
